# C# 异步编程（阶段 25，Step 25.1~25.5）

> 2026-11-18~19 主线：async/await → WhenAll/WhenAny → 线程模型 → CancellationToken → 并发控制。
> 实验代码：`Controllers/AsyncDemoController.cs`（sync/async/when-all/thread/task-run/thread-pool/cancellation/race-condition/interlocked/semaphore 等接口）
> 配套：[消息队列与幂等](08-消息队列与幂等-阶段28.md) · [分布式与DDD](09-分布式与DDD-阶段29.md) · [路线图](03-问题清单与学习路线图.md)

---

## 1. async/await 基础（25.1）

**核心结论：async/await 不是多线程。**

```csharp
// 同步：阻塞线程 3 秒（Thread.Sleep 占住线程）
[HttpGet("sync")] public IActionResult Sync() { Thread.Sleep(3000); ... }

// 异步：不占线程（Task.Delay 是定时器回调）
[HttpGet("async")]
public async Task<IActionResult> Async()
{
    var startThreadId = Environment.CurrentManagedThreadId;
    await Task.Delay(3000);
    var endThreadId = Environment.CurrentManagedThreadId;   // ASP.NET Core 无 SynchronizationContext
    ...
}
```

- 实测 `startThreadId == endThreadId`（如都是 15）**是正常的** —— 恢复处可能复用同一线程，不代表没异步
- **连续 await 是串行**：`await A(); await B();` 总耗时 = A+B
- `Task.Delay` 不占线程；真正的异步 I/O（网络/磁盘）由完成端口回调驱动，**这是高并发的基础**

## 2. Task.WhenAll / WhenAny（25.2）

```csharp
var t1 = Task.Delay(2000); var t2 = Task.Delay(3000); var t3 = Task.Delay(4000);
await Task.WhenAll(t1, t2, t3);   // 实测 elapsed = 4001ms → 取最慢者
```

**五道面试题结论**：
1. WhenAll 三个 Delay(2/3/4s) → **并发执行，总耗时 ≈ 4s**（不是 9s）
2. **WhenAll 不创建线程** —— 只是等待已启动的任务集合
3. **某个任务抛异常：其余继续执行**，`await WhenAll` 在全部结束后才抛；用 `allTask.Exception.Flatten().InnerExceptions` 取全部异常
4. **DbContext 非线程安全**（`A second operation was started...`）→ `AddDbContext` 默认 Scoped；并发查询用 **`IDbContextFactory<TContext>`** 每次 `CreateDbContextAsync()`
5. `WhenAll` 中 Task2 失败 → **Task1/Task3 继续执行**，不自动取消

**WhenAny**：只返回第一个完成者（**可能是失败任务**），不取消其余 —— 超时控制要配 CTS：

```csharp
var completed = await Task.WhenAny(realTask, Task.Delay(5000, cts.Token));
if (completed != realTask) { cts.Cancel(); return StatusCode(504); }
```

## 3. Thread / ThreadPool / Task 关系（25.3）

| 概念 | 本质 | 要点 |
| --- | --- | --- |
| `Thread` | 直接创建 OS 线程 | 重资源；`new Thread` **不受 ThreadPool 管理** |
| ThreadPool | 复用工作线程的池子 | `QueueUserWorkItem` 提交 Work Item、不返回 Task 不能 await；`GetAvailable/Max/MinThreads` 查状态（Available 是剩余额度非运行数） |
| `Task.Run` | 把工作项提交线程池 | **不创建 Thread**；是任务抽象可 await |

- **.NET 没有 goroutine 等价物** —— async/await 编译为**状态机**（不是新线程），异步 I/O 不占线程
- **ThreadPool Starvation（线程池饥饿）**：线程被同步阻塞（`.Result`/`.Wait()`/`Thread.Sleep`/同步 I/O）→ 新请求排队 → 吞吐骤降
- **反模式**：`Task.Run(() => _context.Orders.ToList())` 用线程池线程包同步 I/O —— 正确写法 `await _context.Orders.ToListAsync()`

## 4. CancellationToken 协作式取消（25.4）

```csharp
using var cts = new CancellationTokenSource();
try { await Task.Delay(3000, cts.Token); }
catch (OperationCanceledException) { /* 取消 */ }

// 双触发：客户端断开 + 5 秒超时
var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutToken);
cts.CancelAfter(TimeSpan.FromSeconds(5));      // 定时取消

// 一任务失败立即取消其余：catch 里 cts.Cancel() 再 throw
```

- **协作式取消**（不是强杀）：任务必须主动检查 token（`ThrowIfCancellationRequested()`）才能响应
- MVC 里 action 的 `CancellationToken` 参数**自动绑定 `HttpContext.RequestAborted`**
- `WhenAll` 失败**不会自动取消**其余任务，需任务内部 catch 后手动 `cts.Cancel()`

## 5. 并发控制：竞态 / lock / Interlocked / SemaphoreSlim（25.5）

```csharp
// 竞态：count++ 是读-改-写三步非原子
Parallel.For(0, 100000, _ => _count++);        // 实测 ≈ 76321（丢了 ~24%）
Parallel.For(0, 100000, _ => { lock (_countLock) { _count++; } });  // 实测 = 100000

// 原子操作（无锁，仅适合简单计数）
Interlocked.Increment(ref _count);
Interlocked.Add / Exchange / CompareExchange

// 信号量：限并发数（10 任务、上限 3，实测 active 恒 ≤ 3）
private static readonly SemaphoreSlim _sem = new(3, 3);
await _sem.WaitAsync(ct);
try { ... } finally { _sem.Release(); }        // 必须 finally 释放！
```

**要点**：
- 锁对象用 `private static readonly object _lock = new()`（不能锁 `this`/`string`/类型对象）
- **锁内不做 I/O**（会把临界区变长，等同串行）；`lock` 是**进程级**的，多实例部署无效（要分布式锁）
- `SemaphoreSlim` 是异步友好版信号量（`WaitAsync`），`Mutex` 才是跨进程的

## 6. 面试速记卡

- 「async 一定多线程吗？」→ 不是；状态机 + 线程池恢复，异步 I/O 不占线程
- 「WhenAll 一个失败其余会怎样？」→ 继续执行，全部结束后 await 才抛
- 「线程池饥饿怎么排查/避免？」→ 同步阻塞占满工作线程；全链路 async、禁 `.Result/.Wait()`
- 「并发查库怎么办？」→ `IDbContextFactory` 每操作一个 Context
- 「取消为什么不生效？」→ 协作式，任务没检查 token
- 「lock 为什么保护不了多实例？」→ 进程内锁；分布式场景用 Redis 分布式锁

## 7. 实验接口清单（AsyncDemoController）

| 路由 | 演示 |
| --- | --- |
| `GET /api/asyncdemo/sync` vs `/async` | 阻塞 vs 异步（ThreadId 对比） |
| `GET /api/asyncdemo/async-parallel`、`/when-all` | WhenAll 并发耗时 |
| `GET /api/asyncdemo/thread`、`/task-run` | Thread vs Task.Run 线程 ID |
| `GET /api/asyncdemo/thread-pool`、`/thread-pool-info` | ThreadPool 状态 |
| `GET /api/asyncdemo/cancellation-source`、`/parallel-cancellation`、`/cancel-on-failure` | CTS / 链接 Token / 失败联动 |
| `GET /api/asyncdemo/race-condition`、`/race-condition-lock` | 竞态丢失 vs lock 修复 |
| `GET /api/asyncdemo/interlocked`、`/semaphore` | 原子操作 / 信号量限流 |
