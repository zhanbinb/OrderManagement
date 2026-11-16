### 

完成一个 ASP.NET Core Web API 小项目，覆盖 DI、EF Core、验证、事务，异常处理、日志和 SQL，或其它现在.net core中 经常使用的技术。
为面试.net 岗位，恢复编码能力，做准备。带着我，一步一步的完成。我的编码环境是 Mac 的 VS code 或 Rider， .net 看选用 9 还是10。你还有什么问题，可以先问我。

---

### 

我建议我们把这件事当成一个真正的 **.NET 后端项目实战 + 面试恢复训练** 来做，而不是单纯写几个 Demo。

我们的目标是：通过一个完整、可运行、逐步迭代的 ASP.NET Core Web API 项目，把你过去熟悉的 C#/.NET 后端开发能力重新激活，同时补齐近几年 .NET Core 项目中常见的开发模式和面试知识点。

我会采用你之前学习 Agent 时比较适合的方式：

1. **先明确项目架构**，不一上来就堆代码。
2. 每一步先讲解为什么这么设计，再动手实现。
3. 每个阶段都有可以运行、测试的代码。
4. 重要代码解释到位，尤其是 DI、EF Core、异步、事务、中间件等。
5. 每完成一个模块，配套整理面试题和实际开发中容易踩的坑。
6. 最终形成一个可以放到 GitHub、用于面试讲解的完整项目。

## 一、项目选型

我建议做一个：

**企业订单管理系统（Order Management API）**

它足够贴近企业级后台开发，又不会像电商完整系统一样过于庞大。

主要业务包括：

- 用户和客户管理
- 商品管理
- 订单创建、查询、修改
- 订单明细管理
- 库存扣减
- 订单状态流转
- 操作日志和异常记录

通过订单创建这个核心业务，我们可以自然地引入数据库事务、并发控制、数据验证、异常处理等内容。

### 技术栈规划

| 技术 | 选型 | 学习重点 |
|---|---|---|
| 开发框架 | ASP.NET Core Web API | Controller、Middleware、路由 |
| 开发语言 | C# | 泛型、LINQ、异步、委托 |
| ORM | EF Core | DbContext、LINQ、Tracking |
| 数据库 | PostgreSQL | SQL、索引、事务 |
| 数据库迁移 | EF Core Migration | Code First |
| 依赖注入 | 内置 DI | 生命周期、构造注入 |
| 参数验证 | FluentValidation | DTO 验证 |
| 日志 | Serilog | 结构化日志 |
| API 文档 | Swagger / OpenAPI | 接口调试、文档 |
| 异常处理 | Exception Handler | 全局异常处理、ProblemDetails |
| 缓存 | Redis | Cache-Aside |
| 测试 | xUnit | 单元测试、集成测试 |
| 容器 | Docker Compose | 本地开发环境 |

不是第一天就全部实现，而是按照业务需要逐步引入。



---

### 

我建议我们把这件事当成一个真正的 **.NET 后端项目实战 + 面试恢复训练** 来做，而不是单纯写几个 Demo。

我们的目标是：通过一个完整、可运行、逐步迭代的 ASP.NET Core Web API 项目，把你过去熟悉的 C#/.NET 后端开发能力重新激活，同时补齐近几年 .NET Core 项目中常见的开发模式和面试知识点。

我会采用你之前学习 Agent 时比较适合的方式：

1. **先明确项目架构**，不一上来就堆代码。
2. 每一步先讲解为什么这么设计，再动手实现。
3. 每个阶段都有可以运行、测试的代码。
4. 重要代码解释到位，尤其是 DI、EF Core、异步、事务、中间件等。
5. 每完成一个模块，配套整理面试题和实际开发中容易踩的坑。
6. 最终形成一个可以放到 GitHub、用于面试讲解的完整项目。

## 一、项目选型

我建议做一个：

**企业订单管理系统（Order Management API）**

它足够贴近企业级后台开发，又不会像电商完整系统一样过于庞大。

主要业务包括：

- 用户和客户管理
- 商品管理
- 订单创建、查询、修改
- 订单明细管理
- 库存扣减
- 订单状态流转
- 操作日志和异常记录

通过订单创建这个核心业务，我们可以自然地引入数据库事务、并发控制、数据验证、异常处理等内容。

### 技术栈规划

| 技术 | 选型 | 学习重点 |
|---|---|---|
| 开发框架 | ASP.NET Core Web API | Controller、Middleware、路由 |
| 开发语言 | C# | 泛型、LINQ、异步、委托 |
| ORM | EF Core | DbContext、LINQ、Tracking |
| 数据库 | PostgreSQL | SQL、索引、事务 |
| 数据库迁移 | EF Core Migration | Code First |
| 依赖注入 | 内置 DI | 生命周期、构造注入 |
| 参数验证 | FluentValidation | DTO 验证 |
| 日志 | Serilog | 结构化日志 |
| API 文档 | Swagger / OpenAPI | 接口调试、文档 |
| 异常处理 | Exception Handler | 全局异常处理、ProblemDetails |
| 缓存 | Redis | Cache-Aside |
| 测试 | xUnit | 单元测试、集成测试 |
| 容器 | Docker Compose | 本地开发环境 |

不是第一天就全部实现，而是按照业务需要逐步引入。

## 二、选择 .NET 9 还是 .NET 10？

<badge color="success">推荐 .NET 10 LTS</badge>

截至 2026 年 10 月，微软官方支持情况：

| 版本 | 类型 | 结束支持时间 |
|---|---|---|
| .NET 10 | LTS | 2028-11 |
| .NET 9 | STS | 2026-11 |
| .NET 8 | LTS | 2026-11 |

.NET 10 已经是正式稳定版本，而且支持周期更长。因此没有必要为了学习而选择 .NET 9。<Cite refs={["turn242038search1","turn242038search8"]}/>

我们统一采用：

- .NET 10
- C# 14
- ASP.NET Core 10
- EF Core 10
- FluentValidation
- Serilog
- xUnit

官方参考：[.NET 10 文档](https://learn.microsoft.com/zh-cn/dotnet/core/whats-new/dotnet-10/overview)

## 三、项目分阶段实施

我不建议一开始就使用复杂的 Clean Architecture，也不建议一开始拆成多个微服务。

先做一个结构清晰的模块化单体项目，符合大多数企业 .NET 后端岗位的实际开发要求。

### 第一阶段：基础 Web API

目标：恢复 ASP.NET Core 开发手感。

| 序号 | 内容 | 重点 |
|---|---|---|
| 01 | 创建项目 | SDK、项目结构、Program.cs |
| 02 | Controller 和路由 | ControllerBase、HTTP 方法、REST |
| 03 | DI 依赖注入 | Transient、Scoped、Singleton |
| 04 | 配置管理 | appsettings、Options Pattern |
| 05 | EF Core | DbContext、Entity、Migration |
| 06 | CRUD | LINQ、异步查询、分页 |
 
### 第二阶段：企业业务开发

目标：掌握日常后端业务开发。

| 序号 | 内容 | 重点 |
|---|---|---|
| 07 | DTO 设计 | Request、Response、Entity 分离 |
| 08 | 参数验证 | FluentValidation |
| 09 | 统一响应 | ApiResponse、分页响应 |
| 10 | 全局异常处理 | ExceptionHandler、ProblemDetails |
| 11 | 日志 | Serilog、结构化日志 |
| 12 | 业务分层 | Controller、Service、Repository |

### 第三阶段：数据库与事务

目标：具备企业级业务处理能力。

| 序号 | 内容 | 重点 |
|---|---|---|
| 13 | EF Core 事务 | BeginTransaction、Commit、Rollback |
| 14 | 订单业务 | 订单、订单明细、库存 |
| 15 | 并发控制 | 乐观锁、ConcurrencyToken |
| 16 | SQL 优化 | JOIN、索引、执行计划 |
| 17 | EF Core 性能 | Tracking、NoTracking、N+1 |
| 18 | 分页优化 | Skip/Take、Keyset Pagination |

### 第四阶段：企业常用技术

| 序号 | 内容 | 重点 |
|---|---|---|
| 19 | Redis | 缓存、过期、缓存一致性 |
| 20 | JWT | 身份认证、授权、角色权限 |
| 21 | Middleware | 请求日志、请求上下文 |
| 22 | BackgroundService | 后台任务 |
| 23 | HttpClientFactory | 外部 API 调用、重试 |
| 24 | xUnit | 单元测试、集成测试 |
| 25 | Docker | API、数据库、Redis 部署 |

最后再做一次完整的代码重构，整理成适合面试展示的项目。

## 四、项目目录规划

最终项目可以采用下面的结构：

```text
OrderManagement/
│
├── src/
│   ├── OrderManagement.Api/
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Extensions/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── OrderManagement.Application/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   ├── Validators/
│   │   └── Interfaces/
│   │
│   ├── OrderManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Exceptions/
│   │
│   └── OrderManagement.Infrastructure/
│       ├── Persistence/
│       ├── Repositories/
│       └── Configurations/
│
├── tests/
│   ├── OrderManagement.UnitTests/
│   └── OrderManagement.IntegrationTests/
│
├── docker-compose.yml
└── README.md
```

注意：这只是最终目标结构。前期我们会先从一个 Web API 项目开始，随着业务复杂度增加，再拆分项目。避免为了架构而架构。

## 五、开始之前，我需要和你确认 3 个问题

我有一些之前的背景：你做过多年 C#、ASP.NET MVC、.NET Core 和 SQL Server 项目，但最近几年实际编码相对少一些。因此，我们不需要从 C# 基础语法开始，而是重点恢复现代 ASP.NET Core 开发能力。

{@body const [ide,setIde] = DIL.useState("Rider 为主，VS Code 辅助")}
{@body const [db,setDb] = DIL.useState("PostgreSQL（推荐）")}
{@body const [pace,setPace] = DIL.useState("我自己动手写，你逐步指导、检查和纠错")}
{@body const [extra,setExtra] = DIL.useState("没有，按你的推荐方案")}
<box border radius="lg" padding={3} gap={3}>
  **1. 你准备使用哪个 IDE？**
  <radio-group direction="col" value={ide} onChange={setIde}>
    <radio value="Rider 为主，VS Code 辅助">Rider 为主，VS Code 辅助</radio>
    <radio value="VS Code 为主，Rider 辅助">VS Code 为主，Rider 辅助</radio>
    <radio value="主要使用 VS Code">主要使用 VS Code</radio>
    <radio value="主要使用 Rider">主要使用 Rider</radio>
  </radio-group>
  <divider color="subtle"/>
  **2. 数据库选择？**
  <radio-group direction="col" value={db} onChange={setDb}>
    <radio value="PostgreSQL（推荐）">PostgreSQL（推荐，Mac M3 本地 Docker 环境方便）</radio>
    <radio value="SQL Server">SQL Server（贴近过去的 .NET 项目经验）</radio>
    <radio value="MySQL">MySQL</radio>
  </radio-group>
  <divider color="subtle"/>
  **3. 希望我采用哪种教学方式？**
  <radio-group direction="col" value={pace} onChange={setPace}>
    <radio value="我自己动手写，你逐步指导、检查和纠错">我自己动手写，你逐步指导、检查和纠错（最推荐）</radio>
    <radio value="你先提供完整代码，我跟着运行和理解">你先提供完整代码，我跟着运行和理解</radio>
    <radio value="先讲解原理，再一起完成代码">先讲解原理，再一起完成代码</radio>
  </radio-group>
  <divider color="subtle"/>
  **补充要求（可选）**
  <textarea value={extra} onChange={setExtra} rows={2} placeholder="例如：希望多练 SQL、希望结合实际面试题……"/>
  <button block onClick={()=>GenUI.issueNewTurn("项目实施选择：\nIDE："+ide+"\n数据库："+db+"\n教学方式："+pace+"\n补充要求："+extra+"\n请根据这些选择开始第一阶段。")}>确认，开始项目</button>
</box>

**我的额外建议：**

整个项目不要追求代码量，而要追求你能够独立回答这些面试问题：

- ASP.NET Core DI 是如何工作的？三种生命周期有什么区别？
- EF Core 的 DbContext 为什么通常注册为 Scoped？
- SaveChangesAsync 和数据库事务有什么关系？
- EF Core 如何处理并发更新？
- 如何实现全局异常处理？
- IQueryable 和 IEnumerable 有什么区别？
- AsNoTracking 有什么作用？
- 如何避免 N+1 查询？
- 如何分析一条 SQL 为什么执行缓慢？

这些问题都要在项目中实际写过、调试过，而不只是背答案。

**最终标准：不仅能把项目跑起来，还能解释每个技术为什么使用、解决了什么问题，以及有哪些替代方案。**

我们就以 .NET 10 为基准，逐步把你的 .NET 后端编码能力恢复起来。

---

### 

## 二、选择 .NET 9 还是 .NET 10？

<badge color="success">推荐 .NET 10 LTS</badge>

截至 2026 年 10 月，微软官方支持情况：

| 版本 | 类型 | 结束支持时间 |
|---|---|---|
| .NET 10 | LTS | 2028-11 |
| .NET 9 | STS | 2026-11 |
| .NET 8 | LTS | 2026-11 |

.NET 10 已经是正式稳定版本，而且支持周期更长。因此没有必要为了学习而选择 .NET 9。<Cite refs={["turn242038search1","turn242038search8"]}/>

我们统一采用：

- .NET 10
- C# 14
- ASP.NET Core 10
- EF Core 10
- FluentValidation
- Serilog
- xUnit

官方参考：[.NET 10 文档](https://learn.microsoft.com/zh-cn/dotnet/core/whats-new/dotnet-10/overview)

## 三、项目分阶段实施

我不建议一开始就使用复杂的 Clean Architecture，也不建议一开始拆成多个微服务。

先做一个结构清晰的模块化单体项目，符合大多数企业 .NET 后端岗位的实际开发要求。

### 第一阶段：基础 Web API

目标：恢复 ASP.NET Core 开发手感。

| 序号 | 内容 | 重点 |
|---|---|---|
| 01 | 创建项目 | SDK、项目结构、Program.cs |
| 02 | Controller 和路由 | ControllerBase、HTTP 方法、REST |
| 03 | DI 依赖注入 | Transient、Scoped、Singleton |
| 04 | 配置管理 | appsettings、Options Pattern |
| 05 | EF Core | DbContext、Entity、Migration |
| 06 | CRUD | LINQ、异步查询、分页 |
 
### 第二阶段：企业业务开发

目标：掌握日常后端业务开发。

| 序号 | 内容 | 重点 |
|---|---|---|
| 07 | DTO 设计 | Request、Response、Entity 分离 |
| 08 | 参数验证 | FluentValidation |
| 09 | 统一响应 | ApiResponse、分页响应 |
| 10 | 全局异常处理 | ExceptionHandler、ProblemDetails |
| 11 | 日志 | Serilog、结构化日志 |
| 12 | 业务分层 | Controller、Service、Repository |

### 第三阶段：数据库与事务

目标：具备企业级业务处理能力。

| 序号 | 内容 | 重点 |
|---|---|---|
| 13 | EF Core 事务 | BeginTransaction、Commit、Rollback |
| 14 | 订单业务 | 订单、订单明细、库存 |
| 15 | 并发控制 | 乐观锁、ConcurrencyToken |
| 16 | SQL 优化 | JOIN、索引、执行计划 |
| 17 | EF Core 性能 | Tracking、NoTracking、N+1 |
| 18 | 分页优化 | Skip/Take、Keyset Pagination |

### 第四阶段：企业常用技术

| 序号 | 内容 | 重点 |
|---|---|---|
| 19 | Redis | 缓存、过期、缓存一致性 |
| 20 | JWT | 身份认证、授权、角色权限 |
| 21 | Middleware | 请求日志、请求上下文 |
| 22 | BackgroundService | 后台任务 |
| 23 | HttpClientFactory | 外部 API 调用、重试 |
| 24 | xUnit | 单元测试、集成测试 |
| 25 | Docker | API、数据库、Redis 部署 |

最后再做一次完整的代码重构，整理成适合面试展示的项目。

## 四、项目目录规划

最终项目可以采用下面的结构：

```text
OrderManagement/
│
├── src/
│   ├── OrderManagement.Api/
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Extensions/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── OrderManagement.Application/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   ├── Validators/
│   │   └── Interfaces/
│   │
│   ├── OrderManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Exceptions/
│   │
│   └── OrderManagement.Infrastructure/
│       ├── Persistence/
│       ├── Repositories/
│       └── Configurations/
│
├── tests/
│   ├── OrderManagement.UnitTests/
│   └── OrderManagement.IntegrationTests/
│
├── docker-compose.yml
└── README.md
```

注意：这只是最终目标结构。前期我们会先从一个 Web API 项目开始，随着业务复杂度增加，再拆分项目。避免为了架构而架构。



---

### 

## 五、开始之前，我需要和你确认 3 个问题

我有一些之前的背景：你做过多年 C#、ASP.NET MVC、.NET Core 和 SQL Server 项目，但最近几年实际编码相对少一些。因此，我们不需要从 C# 基础语法开始，而是重点恢复现代 ASP.NET Core 开发能力。

{@body const [ide,setIde] = DIL.useState("Rider 为主，VS Code 辅助")}
{@body const [db,setDb] = DIL.useState("PostgreSQL（推荐）")}
{@body const [pace,setPace] = DIL.useState("我自己动手写，你逐步指导、检查和纠错")}
{@body const [extra,setExtra] = DIL.useState("没有，按你的推荐方案")}
<box border radius="lg" padding={3} gap={3}>
  **1. 你准备使用哪个 IDE？**
  <radio-group direction="col" value={ide} onChange={setIde}>
    <radio value="Rider 为主，VS Code 辅助">Rider 为主，VS Code 辅助</radio>
    <radio value="VS Code 为主，Rider 辅助">VS Code 为主，Rider 辅助</radio>
    <radio value="主要使用 VS Code">主要使用 VS Code</radio>
    <radio value="主要使用 Rider">主要使用 Rider</radio>
  </radio-group>
  <divider color="subtle"/>
  **2. 数据库选择？**
  <radio-group direction="col" value={db} onChange={setDb}>
    <radio value="PostgreSQL（推荐）">PostgreSQL（推荐，Mac M3 本地 Docker 环境方便）</radio>
    <radio value="SQL Server">SQL Server（贴近过去的 .NET 项目经验）</radio>
    <radio value="MySQL">MySQL</radio>
  </radio-group>
  <divider color="subtle"/>
  **3. 希望我采用哪种教学方式？**
  <radio-group direction="col" value={pace} onChange={setPace}>
    <radio value="我自己动手写，你逐步指导、检查和纠错">我自己动手写，你逐步指导、检查和纠错（最推荐）</radio>
    <radio value="你先提供完整代码，我跟着运行和理解">你先提供完整代码，我跟着运行和理解</radio>
    <radio value="先讲解原理，再一起完成代码">先讲解原理，再一起完成代码</radio>
  </radio-group>
  <divider color="subtle"/>
  **补充要求（可选）**
  <textarea value={extra} onChange={setExtra} rows={2} placeholder="例如：希望多练 SQL、希望结合实际面试题……"/>
  <button block onClick={()=>GenUI.issueNewTurn("项目实施选择：\nIDE："+ide+"\n数据库："+db+"\n教学方式："+pace+"\n补充要求："+extra+"\n请根据这些选择开始第一阶段。")}>确认，开始项目</button>
</box>

**我的额外建议：**

整个项目不要追求代码量，而要追求你能够独立回答这些面试问题：

- ASP.NET Core DI 是如何工作的？三种生命周期有什么区别？
- EF Core 的 DbContext 为什么通常注册为 Scoped？
- SaveChangesAsync 和数据库事务有什么关系？
- EF Core 如何处理并发更新？
- 如何实现全局异常处理？
- IQueryable 和 IEnumerable 有什么区别？
- AsNoTracking 有什么作用？
- 如何避免 N+1 查询？
- 如何分析一条 SQL 为什么执行缓慢？

这些问题都要在项目中实际写过、调试过，而不只是背答案。

**最终标准：不仅能把项目跑起来，还能解释每个技术为什么使用、解决了什么问题，以及有哪些替代方案。**

我们就以 .NET 10 为基准，逐步把你的 .NET 后端编码能力恢复起来。

---

### 

项目实施选择：
IDE：Rider 为主，VS Code 辅助
数据库：PostgreSQL（推荐）
教学方式：我自己动手写，你逐步指导、检查和纠错
补充要求：Rider 是否需要更新到最新版本？
请根据这些选择开始第一阶段。

---

### 

好，我们正式开始。接下来我会按照**真实企业项目开发的节奏**带你完成，而不是一次性把完整代码交给你。

先确定我们的开发约定：

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **OrderManagement 项目**
    <badge color="success">正式启动</badge>
  </row>
  | 项目项 | 确定方案 |
  |---|---|
  | IDE | Rider 主力，VS Code 辅助 |
  | 开发平台 | macOS / Apple Silicon M3 |
  | .NET | .NET 10 LTS |
  | C# | C# 14 |
  | 数据库 | PostgreSQL |
  | ORM | EF Core 10 |
  | 开发方式 | 自己编码，我负责指导和 Code Review |
  | 项目架构 | 模块化单体，逐步分层 |
</box>

## 0. Rider 是否需要更新？

我的建议是：**先检查当前版本，再决定是否升级，不需要盲目重装。**

主要确认三个方面：

1. 当前 Rider 是否支持 .NET 10 和 C# 14。
2. 是否支持最新的 ASP.NET Core 项目模板、调试和 NuGet 管理。
3. 是否能正确识别 Apple Silicon 和本机 .NET SDK。



---

### 

好，我们正式开始。接下来我会按照**真实企业项目开发的节奏**带你完成，而不是一次性把完整代码交给你。

先确定我们的开发约定：

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **OrderManagement 项目**
    <badge color="success">正式启动</badge>
  </row>
  | 项目项 | 确定方案 |
  |---|---|
  | IDE | Rider 主力，VS Code 辅助 |
  | 开发平台 | macOS / Apple Silicon M3 |
  | .NET | .NET 10 LTS |
  | C# | C# 14 |
  | 数据库 | PostgreSQL |
  | ORM | EF Core 10 |
  | 开发方式 | 自己编码，我负责指导和 Code Review |
  | 项目架构 | 模块化单体，逐步分层 |
</box>

## 0. Rider 是否需要更新？

我的建议是：**先检查当前版本，再决定是否升级，不需要盲目重装。**

主要确认三个方面：

1. 当前 Rider 是否支持 .NET 10 和 C# 14。
2. 是否支持最新的 ASP.NET Core 项目模板、调试和 NuGet 管理。
3. 是否能正确识别 Apple Silicon 和本机 .NET SDK。

根据官方资料，Rider 2025.3 已经提供 .NET 10 和 C# 14 的正式支持；Rider 2026.1 又进一步增强了 C# 14 支持。<Cite refs={["turn521409search1","turn521409search2"]}/>

我的建议：

- 如果你当前是 Rider 2025.3 或更早：建议升级。
- 如果已经是 Rider 2026.1：可以直接使用。
- 如果有更新的 Stable 正式版本：优先升级到最新 Stable，不建议使用 EAP 预览版。

macOS 操作路径：

`JetBrains Rider → Check for Updates`

官方说明：[Rider 更新指南](https://www.jetbrains.com/help/rider/Update.html)

---

## 六、第一阶段：环境检查与项目初始化

<badge color="success">Step 01 · 环境准备</badge>

今天先完成一件事：确保本机 .NET 10 开发环境正常。

**第一步：检查当前环境**

打开 Mac 终端（Rider Terminal 也可以），依次执行：

```bash
# 检查 Mac CPU 架构
uname -m

# 查看 .NET SDK 信息
dotnet --info

# 查看已安装的 SDK
dotnet --list-sdks

# 查看已安装的 Runtime
dotnet --list-runtimes
```

你的 MacBook Air M3 应该显示：

```text
arm64
```

如果没有安装 .NET 10 SDK：

进入微软官方页面：

<Link url="https://dotnet.microsoft.com/zh-cn/download/dotnet/10.0" title=".NET 10 SDK 下载"/>

选择：

- macOS
- Arm64
- SDK（不是单独的 Runtime）

SDK 已经包含 ASP.NET Core Runtime，不需要单独安装。<Cite refs={["turn521409search3","turn521409search4"]}/>

**第二步：检查 Rider**

打开 Rider，查看：

`Rider → About JetBrains Rider`

记录当前版本号。

**第三步：检查项目目录**

建议在 Mac 上统一创建一个独立的 .NET 项目工作区：

```bash
mkdir -p ~/code/dotnet
cd ~/code/dotnet
```

暂时不要创建项目，先确认 SDK 和 Rider 环境。

---

## 七、我们接下来的第一个项目

环境确认后，我们正式创建：

```text
~/code/dotnet/
└── OrderManagement/
    ├── OrderManagement.sln
    └── src/
        └── OrderManagement.Api/
            ├── Controllers/
            ├── Program.cs
            ├── appsettings.json
            └── OrderManagement.Api.csproj
```

第一版先使用 ASP.NET Core Web API Controller 模式，不使用 Minimal API。

我们会亲自编写第一个 Controller，理解：

- `Program.cs` 到底做了什么？
- `WebApplication.CreateBuilder()` 是什么？
- `builder.Services` 是如何注册服务的？
- `builder.Build()` 做了什么？
- `app.MapControllers()` 如何将请求映射到 Controller？
- 一个 HTTP 请求是如何进入 Controller 的？

这些是后面学习 DI、Middleware、异常处理的基础。

**现在先完成 Step 01。**

请把下面两项结果发给我：

1. `dotnet --info` 和 `dotnet --list-sdks` 的输出。
2. Rider 当前版本号。

我检查通过后，就带你创建第一个 ASP.NET Core 10 Web API 项目。我们一次只推进一个步骤，每一步都保证你自己能运行、能理解、能解释。

---

### 

根据官方资料，Rider 2025.3 已经提供 .NET 10 和 C# 14 的正式支持；Rider 2026.1 又进一步增强了 C# 14 支持。<Cite refs={["turn521409search1","turn521409search2"]}/>

我的建议：

- 如果你当前是 Rider 2025.3 或更早：建议升级。
- 如果已经是 Rider 2026.1：可以直接使用。
- 如果有更新的 Stable 正式版本：优先升级到最新 Stable，不建议使用 EAP 预览版。

macOS 操作路径：

`JetBrains Rider → Check for Updates`

官方说明：[Rider 更新指南](https://www.jetbrains.com/help/rider/Update.html)

---

## 六、第一阶段：环境检查与项目初始化

<badge color="success">Step 01 · 环境准备</badge>

今天先完成一件事：确保本机 .NET 10 开发环境正常。

**第一步：检查当前环境**

打开 Mac 终端（Rider Terminal 也可以），依次执行：

```bash
# 检查 Mac CPU 架构
uname -m

# 查看 .NET SDK 信息
dotnet --info

# 查看已安装的 SDK
dotnet --list-sdks

# 查看已安装的 Runtime
dotnet --list-runtimes
```

你的 MacBook Air M3 应该显示：

```text
arm64
```

如果没有安装 .NET 10 SDK：

进入微软官方页面：

<Link url="https://dotnet.microsoft.com/zh-cn/download/dotnet/10.0" title=".NET 10 SDK 下载"/>

选择：

- macOS
- Arm64
- SDK（不是单独的 Runtime）

SDK 已经包含 ASP.NET Core Runtime，不需要单独安装。<Cite refs={["turn521409search3","turn521409search4"]}/>

**第二步：检查 Rider**

打开 Rider，查看：

`Rider → About JetBrains Rider`

记录当前版本号。

**第三步：检查项目目录**

建议在 Mac 上统一创建一个独立的 .NET 项目工作区：

```bash
mkdir -p ~/code/dotnet
cd ~/code/dotnet
```

暂时不要创建项目，先确认 SDK 和 Rider 环境。

---

## 七、我们接下来的第一个项目

环境确认后，我们正式创建：

```text
~/code/dotnet/
└── OrderManagement/
    ├── OrderManagement.sln
    └── src/
        └── OrderManagement.Api/
            ├── Controllers/
            ├── Program.cs
            ├── appsettings.json
            └── OrderManagement.Api.csproj
```

第一版先使用 ASP.NET Core Web API Controller 模式，不使用 Minimal API。

我们会亲自编写第一个 Controller，理解：

- `Program.cs` 到底做了什么？
- `WebApplication.CreateBuilder()` 是什么？
- `builder.Services` 是如何注册服务的？
- `builder.Build()` 做了什么？
- `app.MapControllers()` 如何将请求映射到 Controller？
- 一个 HTTP 请求是如何进入 Controller 的？

这些是后面学习 DI、Middleware、异常处理的基础。

**现在先完成 Step 01。**

请把下面两项结果发给我：

1. `dotnet --info` 和 `dotnet --list-sdks` 的输出。
2. Rider 当前版本号。

我检查通过后，就带你创建第一个 ASP.NET Core 10 Web API 项目。我们一次只推进一个步骤，每一步都保证你自己能运行、能理解、能解释。

---

### 

开始下一步

---

### 

好，我们进入 **Step 02：创建 ASP.NET Core 10 Web API 项目**。

这一步先不引入 EF Core、PostgreSQL 等依赖。先把一个最基础的 Web API 跑起来，理解 ASP.NET Core 的项目结构和启动流程。

## 1. 创建项目目录

打开 Rider Terminal 或 macOS 终端：

```bash
cd ~/code/dotnet

mkdir -p OrderManagement/src

cd OrderManagement
```

## 2. 创建 Solution 和 Web API

先创建解决方案：

```bash
dotnet new sln -n OrderManagement
```

然后创建 ASP.NET Core Web API：

```bash
dotnet new webapi \
  -n OrderManagement.Api \
  -o src/OrderManagement.Api \
  --framework net10.0 \
  --use-controllers
```

将项目加入解决方案：

```bash
dotnet sln add src/OrderManagement.Api/OrderManagement.Api.csproj
```

这里解释一下：

| 命令 | 作用 |
|---|---|
| `dotnet new sln` | 创建解决方案 |
| `dotnet new webapi` | 创建 Web API 项目 |
| `--framework net10.0` | 指定 .NET 10 |
| `--use-controllers` | 使用 Controller 模式 |
| `dotnet sln add` | 将项目加入解决方案 |

注意：我们明确使用 Controller，而不是 Minimal API。

## 3. 使用 Rider 打开项目

打开 Rider：

1. 选择 Open。
2. 找到 `~/code/dotnet/OrderManagement`。
3. 打开 `OrderManagement.sln`。
4. 等待 Rider 加载项目和 NuGet 依赖。

最终目录应该类似：

```text
OrderManagement/
├── OrderManagement.sln
└── src/
    └── OrderManagement.Api/
        ├── Controllers/
        │   └── WeatherForecastController.cs
        ├── Properties/
        │   └── launchSettings.json
        ├── appsettings.json
        ├── appsettings.Development.json
        ├── OrderManagement.Api.csproj
        └── Program.cs
```

## 4. 理解 Program.cs

打开 `Program.cs`。

你应该看到类似这样的代码（具体内容以 SDK 生成的模板为准）：

```csharp
var builder = WebApplication.CreateBuilder(args);

// 注册 MVC Controller 服务
builder.Services.AddControllers();

// Swagger / OpenAPI 服务
builder.Services.AddOpenApi();

var app = builder.Build();

// 配置 HTTP 请求管道
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

这里有两个非常重要的对象：

**builder：负责应用程序构建之前的配置。**

例如：

```csharp
builder.Services.AddControllers();
builder.Services.AddScoped<IOrderService, OrderService>();
```

**app：负责应用程序构建之后的配置和运行。**

例如：

```csharp
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
```

你可以先记住：

- `builder.Services`：依赖注入容器的服务注册。
- `app`：HTTP 请求处理中间件管道。
- `app.Run()`：启动 Web 应用。

后面我们会专门深入 DI 和 Middleware。

## 5. 运行项目

在 Rider 中可以点击顶部运行按钮。

也可以在终端执行：

```bash
cd src/OrderManagement.Api

dotnet run
```

如果启动成功，会看到类似：

```text
Now listening on: https://localhost:xxxx
Now listening on: http://localhost:xxxx
Application started.
```

端口以你实际运行结果为准。

## 6. 测试第一个 API

模板默认有一个：

```text
GET /WeatherForecast
```

打开浏览器访问：

```text
http://localhost:你的端口/WeatherForecast
```

如果返回 JSON 数组，就说明 API 已经正常运行。

---

## 7. 第一个动手练习：自己创建 Controller

现在不要直接复制我的代码。

请在 `Controllers` 文件夹下创建：

`HelloController.cs`

尝试自己完成下面的要求：

1. 创建一个继承 `ControllerBase` 的 Controller。
2. 使用 `[ApiController]`。
3. 使用 `[Route("api/[controller]")]`。
4. 创建一个 GET 方法。
5. 返回一个字符串：`Hello ASP.NET Core 10!`

目标访问地址：

```http
GET /api/hello
```

返回结果：

```json
{
  "message": "Hello ASP.NET Core 10!"
}
```

提示：可以参考模板中的 `WeatherForecastController.cs`，但尽量自己写。

---

## 本阶段验收

{@body const [a,setA] = DIL.useState(false)}
{@body const [b,setB] = DIL.useState(false)}
{@body const [c,setC] = DIL.useState(false)}
{@body const [d,setD] = DIL.useState(false)}
<box border radius="lg" padding={3} gap={2}>
  **Step 02 完成情况**
  <checkbox checked={a} onChange={setA}>成功创建 OrderManagement.sln</checkbox>
  <checkbox checked={b} onChange={setB}>成功创建并运行 ASP.NET Core 10 API</checkbox>
  <checkbox checked={c} onChange={setC}>理解 Program.cs 的基本结构</checkbox>
  <checkbox checked={d} onChange={setD}>自己完成 HelloController</checkbox>
  <text color="secondary" size="xs">完成后可以直接告诉我，或者贴出代码和报错。</text>
  <button block disabled={!a || !b || !c || !d} onClick={()=>GenUI.issueNewTurn("Step 02 已完成：Solution、Web API、Program.cs 和 HelloController 均已完成。请进入下一步。")}>提交本阶段验收</button>
</box>

**我的要求：** 先不要急着往后赶。你现在的重点是恢复手写 C# 和 ASP.NET Core 的感觉。

完成后把 `HelloController.cs` 和 `Program.cs` 发给我，我会按企业项目代码 Review 的标准检查，然后进入下一步：**深入理解 ASP.NET Core DI 依赖注入，以及三种生命周期。**

---

### 

Step 02 已完成：Solution、Web API、Program.cs 和 HelloController 均已完成。请进入下一步。

---

### 

很好，进入 **Step 03：ASP.NET Core 依赖注入（Dependency Injection，DI）**。

这是 .NET 后端面试的高频考点，也是 ASP.NET Core 最核心的基础设施之一。

这一阶段不只是学习 `AddScoped()`、`AddTransient()`、`AddSingleton()`，我们要自己写一个业务服务，观察它们的实例创建过程，理解为什么企业项目通常使用 Scoped。

# Step 03：依赖注入 DI

<badge color="success">本阶段目标</badge>

完成后，你应该能够：

1. 理解 DI 和 IoC 的区别。
2. 理解 ASP.NET Core 内置 DI 容器。
3. 掌握三种生命周期。
4. 理解构造函数注入。
5. 理解服务注册和服务解析。
6. 理解 DI 生命周期常见错误。
7. 能够独立设计一个 Service 并注入 Controller。

## 1. 先理解 DI 是什么

假设我们有一个订单业务：

```csharp
public class OrderService
{
    public string GetOrder()
    {
        return "Order-001";
    }
}
```

现在 Controller 要调用它。

一种传统写法：

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController()
    {
        _orderService = new OrderService();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_orderService.GetOrder());
    }
}
```

这种方式有什么问题？

假设未来 `OrderService` 依赖：

- PostgreSQL 数据库
- Redis
- 日志服务
- 其他业务服务

那么 Controller 就需要负责创建这些对象，甚至还要负责它们之间的依赖关系。

这会产生几个问题：

- Controller 和具体实现强耦合。
- 对象创建逻辑分散。
- 不方便单元测试。
- 对象生命周期难以统一管理。

### DI 改造

让 ASP.NET Core 的 DI 容器负责创建对象。

Controller 只声明自己需要什么：

```csharp
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
}
```

在 `Program.cs` 注册：

```csharp
builder.Services.AddScoped<OrderService>();
```

这就是构造函数注入。

**核心思想：对象的创建和依赖关系管理交给外部容器，使用者只需要声明依赖。**

## 2. IoC 和 DI 的区别

面试经常问。

| 概念 | 解释 |
|---|---|
| IoC | Inversion of Control，控制反转，是一种设计思想 |
| DI | Dependency Injection，依赖注入，是实现 IoC 的常见方式 |
| DI Container | 依赖注入容器，负责创建和管理对象 |

举个例子：

传统模式：

```text
Controller
    |
    | 主动创建
    v
OrderService
```

DI 模式：

```text
             DI Container
              /       \
             v         v
       Controller   OrderService
             |
             v
       使用 OrderService
```

注意：DI 并不意味着所有对象都必须通过容器创建。它主要解决应用服务及其依赖的管理问题。

## 3. 三种生命周期（重点）

ASP.NET Core 内置 DI 提供三种主要生命周期。

<box border radius="lg" gap={3} padding={3}>
  <box gap={1}>
    <row align="center" justify="between">
      **Transient**
      <badge color="info">每次解析创建新实例</badge>
    </row>
    每次从容器请求服务时，创建一个新的实例。
    ```csharp
    builder.Services.AddTransient<IOrderService, OrderService>();
    ```
    适合轻量、无状态的服务。
  </box>
  <divider color="subtle"/>
  <box gap={1}>
    <row align="center" justify="between">
      **Scoped**
      <badge color="success">每个请求一个实例</badge>
    </row>
    同一个 HTTP 请求范围内共享实例，不同请求创建不同实例。
    ```csharp
    builder.Services.AddScoped<IOrderService, OrderService>();
    ```
    典型场景：业务 Service、EF Core DbContext。
  </box>
  <divider color="subtle"/>
  <box gap={1}>
    <row align="center" justify="between">
      **Singleton**
      <badge color="discovery">整个应用一个实例</badge>
    </row>
    应用程序生命周期内只有一个实例。
    ```csharp
    builder.Services.AddSingleton<IOrderService, OrderService>();
    ```
    适合线程安全、可共享的无状态服务。
  </box>
</box>

### 三种生命周期对比

假设有两个 HTTP 请求，每个请求中都调用两次同一个服务。

| 生命周期 | 请求 1 | 请求 2 |
|---|---|---|
| Transient | A、B | C、D |
| Scoped | A、A | B、B |
| Singleton | A、A | A、A |

这里的 A、B、C、D 代表不同对象实例。

特别注意：

- Scoped 不是每次调用 Controller 创建一次，而是每个 DI Scope 创建一次。
- Singleton 是整个应用容器共享一个实例。
- Transient 是每次解析创建新实例，但在同一个对象图中，已经注入的实例不会自动重新创建。

## 4. 动手实验：观察实例生命周期

现在我们不急着进入订单业务。

先做一个非常小的实验，亲眼观察三种生命周期。

### 第一步：创建接口

在项目中创建目录：

```text
OrderManagement.Api/
└── Services/
    ├── IOperationService.cs
    └── OperationService.cs
```

`IOperationService.cs`：

```csharp
public interface IOperationService
{
    Guid GetOperationId();
}
```

### 第二步：创建实现类

`OperationService.cs`：

```csharp
public class OperationService : IOperationService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetOperationId()
    {
        return _id;
    }
}
```

这里有个知识点：

```csharp
private readonly Guid _id = Guid.NewGuid();
```

Guid 在对象实例创建时生成。

所以：

- 如果是同一个对象，多次调用返回相同 Guid。
- 如果是不同对象，通常返回不同 Guid。

### 第三步：注册三种服务

打开 `Program.cs`，添加：

```csharp
builder.Services.AddTransient<IOperationService, OperationService>();

builder.Services.AddScoped<IOperationService, OperationService>();

builder.Services.AddSingleton<IOperationService, OperationService>();
```

**注意：这里不要直接三行都使用相同接口！**

因为同一个 Service Type 进行多次注册时，单个服务解析默认返回最后一个注册的实现。

因此，实际实验中我们需要使用三个不同接口：

```csharp
builder.Services.AddTransient<ITransientOperationService, OperationService>();

builder.Services.AddScoped<IScopedOperationService, OperationService>();

builder.Services.AddSingleton<ISingletonOperationService, OperationService>();
```

对应创建三个空接口：

```csharp
public interface ITransientOperationService : IOperationService { }

public interface IScopedOperationService : IOperationService { }

public interface ISingletonOperationService : IOperationService { }
```

然后 Controller 分别注入三个接口。

### 第四步：创建 LifecycleController

这一部分请你自己动手完成。

文件：

`Controllers/LifecycleController.cs`

要求：

1. 注入 `ITransientOperationService`。
2. 注入 `IScopedOperationService`。
3. 注入 `ISingletonOperationService`。
4. 在 GET 方法中，分别调用每个服务两次。
5. 返回六个 Guid。

接口地址：

```http
GET /api/lifecycle
```

返回 JSON 结构参考：

```json
{
  "transient1": "...",
  "transient2": "...",
  "scoped1": "...",
  "scoped2": "...",
  "singleton1": "...",
  "singleton2": "..."
}
```

注意：两个请求之间也要比较结果。

预期：

```text
请求 1：
Transient1 != Transient2
Scoped1 == Scoped2
Singleton1 == Singleton2

请求 2：
Transient 与请求 1 不同
Scoped 与请求 1 不同
Singleton 与请求 1 相同
```

---

## 5. 一个非常重要的面试知识点：生命周期依赖问题

假设：

```csharp
builder.Services.AddSingleton<OrderService>();

builder.Services.AddScoped<AppDbContext>();
```

如果 `OrderService` 的构造函数依赖 `AppDbContext`：

```csharp
public class OrderService
{
    public OrderService(AppDbContext dbContext)
    {
    }
}
```

这就产生了一个问题：

Singleton 的生命周期比 Scoped 长。

Singleton 可能在应用启动时创建，并长期持有这个 Scoped 对象。

这会导致 Scoped 服务被错误地跨请求共享。

对于 EF Core 的 DbContext，这尤其危险，因为 DbContext 不是线程安全的，也不应该被多个请求长期共享。

这种情况称为：

**Captive Dependency（依赖生命周期捕获）**

开发环境可以开启验证：

```csharp
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});
```

通常 ASP.NET Core Development 环境已经启用了相关验证。

解决办法：

- 调整依赖服务的生命周期。
- 如果确实需要在 Singleton 中使用 Scoped 服务，通过 `IServiceScopeFactory` 创建独立 Scope。
- 不要为了绕过报错而直接把 DbContext 改成 Singleton。

## 6. 本阶段面试题

先理解，不需要现在背诵。

**Q1：ASP.NET Core DI 有哪三种生命周期？**

答：Transient、Scoped、Singleton。分别代表每次解析创建新实例、每个 Scope 共享实例、整个容器共享实例。

**Q2：为什么 DbContext 通常注册为 Scoped？**

答：一个 HTTP 请求通常对应一个数据库工作单元，同一个请求内共享 DbContext，可以统一跟踪实体和提交修改；不同请求使用不同实例，避免线程安全问题和状态污染。

**Q3：Singleton 能否依赖 Scoped？**

答：不能直接依赖，否则会造成 Scoped 服务被 Singleton 长期持有。需要调整生命周期，或通过 IServiceScopeFactory 显式创建 Scope。

**Q4：Transient 和 Scoped 最大区别是什么？**

答：Transient 每次解析创建新实例；Scoped 在同一个 Scope 内复用实例。

**Q5：DI 容器如何创建对象？**

答：根据服务注册关系解析构造函数依赖，递归创建依赖对象，并根据生命周期管理实例。

---

## 7. 本阶段验收任务

{@body const tasks = ["创建 IOperationService 和 OperationService","创建三个生命周期接口","在 Program.cs 注册三种生命周期","自己完成 LifecycleController","运行接口，比较同一请求和不同请求的 Guid","理解 Singleton 依赖 Scoped 的问题"]}
{@body const [done,setDone] = DIL.useState([])}
<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Step 03 任务清单**
    <badge color={done.length===tasks.length?"success":"secondary"}>{done.length}/{tasks.length}</badge>
  </row>
  <box background="surface-tertiary" height="6px" radius="full" clip>
    <box background="#16A34A" height="6px" radius="full" width={`${done.length/tasks.length*100}%`}/>
  </box>
  {#each tasks as task,i}
    <checkbox key={i} checked={done.includes(i)} onChange={v=>setDone(old=>v?[...old,i]:old.filter(x=>x!==i))}>{task}</checkbox>
  {/each}
</box>

**完成后，请把这三个内容发给我：**

1. `Program.cs` 的服务注册代码。
2. `LifecycleController.cs`。
3. 连续调用两次 `/api/lifecycle` 的结果。

我会检查你的代码，尤其关注构造函数注入、接口设计和生命周期是否正确。

下一阶段我们进入 **Step 04：Options Pattern + appsettings.json 配置管理**，然后再进入 EF Core。这样先把 ASP.NET Core 的基础设施打牢。

---

### 

using Microsoft.AspNetCore.Mvc;\
\
namespace OrderManagement.Api.Controllers;\
\
[ApiController]\
[Route("api/**[controller]**")]\
public class HelloController : ControllerBase\
{\
&#x20;   *// GET*\&#xA;*&#x20;  &#x20;*&#x70;ublic IActionResult Index()\
&#x20;   {\
&#x20;       return  Ok("Hello ASP.NET Core 10!");\
&#x20;   }\
}

---

### 

你的 `HelloController` 整体方向是正确的，已经掌握了 Controller 的基本写法。我们做一次简单的 Code Review。

## 1. 代码检查

你目前的代码：

```csharp
[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    public IActionResult Index()
    {
        return Ok("Hello ASP.NET Core 10!");
    }
}
```

<badge color="success">基本正确</badge>

逐项检查：

| 内容 | 结果 | 说明 |
|---|---|---|
| 继承 ControllerBase | <badge color="success">正确</badge> | API Controller 常用基类 |
| `[ApiController]` | <badge color="success">正确</badge> | 启用 API 专属行为 |
| `[Route]` | <badge color="success">正确</badge> | 定义路由模板 |
| `IActionResult` | <badge color="success">正确</badge> | 灵活返回 HTTP 响应 |
| `Ok()` | <badge color="success">正确</badge> | 返回 HTTP 200 |

## 2. 建议改进的地方

**第一：增加 HTTP 方法特性**

你目前的 `Index()` 没有指定 HTTP 方法。

建议：

```csharp
[HttpGet]
public IActionResult Index()
{
    return Ok(new
    {
        message = "Hello ASP.NET Core 10!"
    });
}
```

这样访问：

```http
GET /api/hello
```

就会得到：

```json
{
  "message": "Hello ASP.NET Core 10!"
}
```

**第二：理解 `[controller]`**

```csharp
[Route("api/[controller]")]
```

`[controller]` 是 ASP.NET Core 的路由令牌，会自动使用 Controller 类名去掉 `Controller` 后缀。

例如：

| Controller 类名 | 路由 |
|---|---|
| HelloController | `/api/hello` |
| OrderController | `/api/order` |
| ProductController | `/api/product` |

因此不需要手动写死 Controller 名称。

## 3. 一个面试知识点

为什么继承 `ControllerBase`，而不是 `Controller`？

| 类型 | 用途 |
|---|---|
| ControllerBase | Web API，提供 HTTP 响应、模型绑定等功能 |
| Controller | MVC，额外提供 View 视图支持 |

我们现在开发的是 RESTful Web API，所以使用 `ControllerBase` 更合适。

---

## 4. 现在继续 Step 03：DI 实战

你刚才发的是 `HelloController`，属于 Step 02 的代码。

我们先完成这部分 Review，然后开始 DI 实验。

请按照上一步的任务创建：

```text
Services/
├── IOperationService.cs
├── OperationService.cs
├── ITransientOperationService.cs
├── IScopedOperationService.cs
└── ISingletonOperationService.cs

Controllers/
└── LifecycleController.cs
```

**这次重点是自己完成 `LifecycleController` 的构造函数注入。**

先不要直接复制完整答案。

写好后把 `Program.cs` 和 `LifecycleController.cs` 发给我，我帮你检查，并通过两次 HTTP 请求验证三种生命周期。

---

### 

我需要Step 03 阶段 LifecycleController 和 Program 的代码，请给我一些代码

---

### 

可以。这次我给你 `Program.cs` 和 `LifecycleController.cs` 的完整参考代码。你先运行、观察结果，再理解每一部分为什么这样写。

我们保持之前的实验设计：通过三个不同接口，观察 Transient、Scoped、Singleton 的实例生命周期。

## 1. 先准备 Services

目录结构：

```text
OrderManagement.Api/
├── Controllers/
│   ├── HelloController.cs
│   └── LifecycleController.cs
│
├── Services/
│   ├── IOperationService.cs
│   ├── OperationService.cs
│   ├── ITransientOperationService.cs
│   ├── IScopedOperationService.cs
│   └── ISingletonOperationService.cs
│
├── Program.cs
└── appsettings.json
```

### 1.1 IOperationService.cs

定义一个公共接口：

```csharp
namespace OrderManagement.Api.Services;

public interface IOperationService
{
    Guid GetOperationId();
}
```

### 1.2 OperationService.cs

```csharp
namespace OrderManagement.Api.Services;

public class OperationService : IOperationService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetOperationId()
    {
        return _id;
    }
}
```

这里需要理解：

```csharp
private readonly Guid _id = Guid.NewGuid();
```

`Guid.NewGuid()` 在对象实例创建时执行。

因此，同一个对象多次调用 `GetOperationId()`，得到的 Guid 不变。

### 1.3 创建三个接口

`ITransientOperationService.cs`

```csharp
namespace OrderManagement.Api.Services;

public interface ITransientOperationService : IOperationService
{
}
```

`IScopedOperationService.cs`

```csharp
namespace OrderManagement.Api.Services;

public interface IScopedOperationService : IOperationService
{
}
```

`ISingletonOperationService.cs`

```csharp
namespace OrderManagement.Api.Services;

public interface ISingletonOperationService : IOperationService
{
}
```

这里使用接口继承，避免重复定义 `GetOperationId()`。

## 2. Program.cs 完整代码

```csharp
using OrderManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. 注册 Controller 服务
builder.Services.AddControllers();

// 2. 注册三种生命周期服务

// 每次解析创建新实例
builder.Services.AddTransient<ITransientOperationService, OperationService>();

// 同一个 Scope 共享实例
builder.Services.AddScoped<IScopedOperationService, OperationService>();

// 整个应用共享实例
builder.Services.AddSingleton<ISingletonOperationService, OperationService>();

// 3. 开启 DI 生命周期验证
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

var app = builder.Build();

// 4. 配置 HTTP 请求管道
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

注意：如果你的项目模板没有使用 `AddOpenApi()`，可以暂时不写 `MapOpenApi()`。这不影响本次 DI 实验。

## 3. LifecycleController.cs 完整代码

```csharp
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Services;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LifecycleController : ControllerBase
{
    private readonly ITransientOperationService _transient;
    private readonly IScopedOperationService _scoped;
    private readonly ISingletonOperationService _singleton;

    // 构造函数注入
    public LifecycleController(
        ITransientOperationService transient,
        IScopedOperationService scoped,
        ISingletonOperationService singleton)
    {
        _transient = transient;
        _scoped = scoped;
        _singleton = singleton;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = new
        {
            Transient1 = _transient.GetOperationId(),
            Transient2 = _transient.GetOperationId(),

            Scoped1 = _scoped.GetOperationId(),
            Scoped2 = _scoped.GetOperationId(),

            Singleton1 = _singleton.GetOperationId(),
            Singleton2 = _singleton.GetOperationId()
        };

        return Ok(result);
    }
}
```

## 4. 运行并测试

启动项目：

```bash
dotnet run
```

访问：

```http
GET http://localhost:5000/api/lifecycle
```

注意：端口以你实际启动时显示的 HTTP 端口为准。

第一次请求，假设返回：

```json
{
  "transient1": "11111111-1111-1111-1111-111111111111",
  "transient2": "22222222-2222-2222-2222-222222222222",
  "scoped1": "33333333-3333-3333-3333-333333333333",
  "scoped2": "33333333-3333-3333-3333-333333333333",
  "singleton1": "44444444-4444-4444-4444-444444444444",
  "singleton2": "44444444-4444-4444-4444-444444444444"
}
```

第二次请求，预期：

| 服务 | 第一次请求 | 第二次请求 |
|---|---|---|
| Transient1 | A | C |
| Transient2 | B | D |
| Scoped1 | E | G |
| Scoped2 | E | G |
| Singleton1 | F | F |
| Singleton2 | F | F |

这就是三种生命周期的核心区别。

## 5. 深入理解一个关键细节

你可能会有一个疑问：

为什么 Transient 在 Controller 中注入一次，后面调用两次，却不会自动创建两个对象？

看这里：

```csharp
private readonly ITransientOperationService _transient;
```

Controller 创建时，DI 容器就已经解析并注入了一个 Transient 实例。

后续：

```csharp
_transient.GetOperationId();
_transient.GetOperationId();
```

只是调用同一个对象两次，并没有再次向 DI 容器请求解析服务。

如果希望观察 Transient 每次解析都创建新实例，可以使用 `IServiceProvider` 显式解析，但这次先不引入这种写法。

---

## 6. 本阶段需要掌握的三个结论

<box border radius="lg" padding={3} gap={3}>
  **Transient**

  <title size="xl" color="default">每次解析一个新实例</title>
  适合轻量、无状态服务。
  <divider color="subtle"/>
  **Scoped**

  <title size="xl" color="success">每个 Scope 共享一个实例</title>
  ASP.NET Core Web API 中通常一个 HTTP 请求对应一个 Scope。DbContext 通常采用这个生命周期。
  <divider color="subtle"/>
  **Singleton**

  <title size="xl" color="default">整个应用共享一个实例</title>
  必须考虑线程安全，不能直接依赖 Scoped 服务。
</box>

## 7. 一个小升级实验

等你先运行成功，我们再做一个扩展：

**让同一个 Controller 注入两个 Transient 实例和两个 Scoped 实例，然后比较它们的 Guid。**

这可以帮助你理解 DI 容器究竟在什么时候创建对象，以及为什么构造函数注入和直接调用 `IServiceProvider.GetRequiredService()` 的行为不同。

你先完成本次实验，把实际两次请求的 JSON 结果发给我。我们再做代码 Review，并进入下一阶段 Options Pattern。

---

### 

我现在返回的结果，测试下来。Transient1 =Transient2，Scoped1=Scoped2 ，每次请求它们的 guid 都会变. Singleton1 =Singleton2，每次请求 GUID 都一样，不变化。

---

### 

你的测试结果完全正确，而且你已经验证了三种 DI 生命周期的核心行为。

不过，有一个非常值得注意的细节：**为什么 Transient1 和 Transient2 相等？这恰恰是理解 DI 的关键。**

## 1. 分析你的测试结果

| 服务 | 同一次请求 | 不同请求 | 判断 |
|---|---|---|---|
| Transient | 相等 | 变化 | 符合预期 |
| Scoped | 相等 | 变化 | 符合预期 |
| Singleton | 相等 | 不变化 | 符合预期 |

三种生命周期都没有问题。

## 2. 为什么 Transient1 = Transient2？

看一下你的 Controller：

```csharp
public class LifecycleController : ControllerBase
{
    private readonly ITransientOperationService _transient;

    public LifecycleController(
        ITransientOperationService transient)
    {
        _transient = transient;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = new
        {
            Transient1 = _transient.GetOperationId(),
            Transient2 = _transient.GetOperationId()
        };

        return Ok(result);
    }
}
```

这里发生了什么？

**第一步：请求进入 Controller。**

ASP.NET Core 创建 Controller 实例。

**第二步：DI 容器解析构造函数参数。**

发现需要 `ITransientOperationService`，于是创建一个 `OperationService` 实例。

**第三步：注入 Controller。**

```csharp
_transient = OperationService实例A;
```

**第四步：调用两次方法。**

```csharp
_transient.GetOperationId();
_transient.GetOperationId();
```

两次调用都是同一个对象，所以 Guid 相同。

注意：

> Transient 是每次从 DI 容器解析服务时创建新实例，而不是每次调用对象方法时创建新实例。

## 3. 做一个升级实验：真正观察 Transient

我们修改 `LifecycleController`，通过 `IServiceProvider` 手动解析服务。

首先在 Controller 构造函数中注入：

```csharp
private readonly IServiceProvider _serviceProvider;
```

修改构造函数：

```csharp
public LifecycleController(
    ITransientOperationService transient,
    IScopedOperationService scoped,
    ISingletonOperationService singleton,
    IServiceProvider serviceProvider)
{
    _transient = transient;
    _scoped = scoped;
    _singleton = singleton;
    _serviceProvider = serviceProvider;
}
```

然后修改 GET 方法：

```csharp
[HttpGet]
public IActionResult Get()
{
    var transient1 = _serviceProvider
        .GetRequiredService<ITransientOperationService>();

    var transient2 = _serviceProvider
        .GetRequiredService<ITransientOperationService>();

    var scoped1 = _serviceProvider
        .GetRequiredService<IScopedOperationService>();

    var scoped2 = _serviceProvider
        .GetRequiredService<IScopedOperationService>();

    var singleton1 = _serviceProvider
        .GetRequiredService<ISingletonOperationService>();

    var singleton2 = _serviceProvider
        .GetRequiredService<ISingletonOperationService>();

    return Ok(new
    {
        Transient1 = transient1.GetOperationId(),
        Transient2 = transient2.GetOperationId(),

        Scoped1 = scoped1.GetOperationId(),
        Scoped2 = scoped2.GetOperationId(),

        Singleton1 = singleton1.GetOperationId(),
        Singleton2 = singleton2.GetOperationId()
    });
}
```

需要添加：

```csharp
using Microsoft.Extensions.DependencyInjection;
```

### 这次预期结果

| 服务 | 第一次解析 | 第二次解析 |
|---|---|---|
| Transient | A | B |
| Scoped | C | C |
| Singleton | D | D |

再次发送 HTTP 请求：

| 服务 | 新请求结果 |
|---|---|
| Transient | 重新创建 |
| Scoped | 重新创建 |
| Singleton | 继续使用 D |

这里就能真正观察到三种生命周期的区别。

另外，这种直接在 Controller 中通过 `IServiceProvider` 解析业务服务的写法，只适合本次实验。实际业务开发优先使用构造函数注入，避免 Service Locator 模式。

---

## 4. 再补充一个企业开发中的重要知识点

为什么 EF Core 的 DbContext 通常使用 Scoped？

因为：

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
```

`AddDbContext` 默认就是 Scoped。

例如一次订单请求：

```text
HTTP Request
    |
    v
OrderController
    |
    v
OrderService
    |
    v
AppDbContext
    |
    v
PostgreSQL
```

同一个请求中，Controller 和 Service 如果依赖同一个 AppDbContext，通常会得到同一个实例。

这样可以让一个业务操作共享实体跟踪状态和数据库工作单元。

但是不同 HTTP 请求会获得不同 DbContext，避免多个请求共享同一个非线程安全的 DbContext。

这也是为什么企业项目中常见：

```csharp
builder.Services.AddScoped<IOrderService, OrderService>();
```

---

## 5. Step 03 阶段验收

目前你已经完成：

- 三种 DI 生命周期注册。
- 构造函数注入。
- 不同 HTTP 请求下的实例观察。
- 理解 Transient 为什么同一次请求中相等。

我建议你再完成上面的 `IServiceProvider` 升级实验。

**完成后，我们就进入 Step 04：Options Pattern 配置管理。**

这一阶段会实际配置：

- appsettings.json
- appsettings.Development.json
- IConfiguration
- IOptions
- IOptionsSnapshot
- IOptionsMonitor
- 强类型配置绑定

这些是现代 ASP.NET Core 企业项目中非常常见的基础知识。

---

### 

好 继续下一步

---

### 

好，我们进入 **Step 04：ASP.NET Core 配置管理与 Options Pattern**。

这一阶段非常重要。真实企业项目中，数据库连接字符串、Redis 地址、JWT 参数、第三方 API 地址等，都需要通过配置管理，而不是写死在代码里。

我们这次以 OrderManagement 项目为基础，实际实现一套配置读取机制。

<box border radius="lg" padding={3} gap={2}>
  **Step 04 学习目标**
  - 理解 `appsettings.json` 和环境配置。
  - 理解 `IConfiguration`。
  - 掌握强类型配置绑定。
  - 掌握 `IOptions<T>`、`IOptionsSnapshot<T>`、`IOptionsMonitor<T>`。
  - 学会通过 DI 注入配置。
  - 为下一阶段 PostgreSQL + EF Core 连接配置做准备。
</box>

## 一、先理解 ASP.NET Core 配置体系

假设我们的订单系统需要配置：

- PostgreSQL 数据库连接字符串
- Redis 地址
- JWT 密钥
- 订单业务参数

传统方式可能会这样写：

```csharp
var connectionString = "Host=localhost;Port=5432;Database=orderdb;...";
```

这存在明显问题：

1. 修改数据库地址需要修改代码。
2. 开发、测试、生产环境配置不同。
3. 密码等敏感信息不应该提交到 Git。
4. 配置项越来越多，难以维护。

ASP.NET Core 提供统一配置系统。

### 1. 配置来源

常见配置来源包括：

| 配置来源 | 典型用途 | 优先级 |
|---|---|---|
| appsettings.json | 通用默认配置 | 低 |
| appsettings.Development.json | 本地开发配置 | 较高 |
| 环境变量 | Docker、服务器部署 | 更高 |
| 命令行参数 | 启动时临时覆盖 | 更高 |
| Secret Manager | 本地开发敏感信息 | 取决于注册顺序 |

默认情况下，ASP.NET Core WebApplication 会注册多个配置提供程序，后注册的配置通常覆盖先注册的同名配置。

例如：

```text
appsettings.json
       |
       v
appsettings.Development.json
       |
       v
Environment Variables
       |
       v
Command Line
```

这里要记住：**配置系统不是只读取 appsettings.json，而是将多个来源合并为一个 IConfiguration。**



---

### 

好，我们进入 **Step 04：ASP.NET Core 配置管理与 Options Pattern**。

这一阶段非常重要。真实企业项目中，数据库连接字符串、Redis 地址、JWT 参数、第三方 API 地址等，都需要通过配置管理，而不是写死在代码里。

我们这次以 OrderManagement 项目为基础，实际实现一套配置读取机制。

<box border radius="lg" padding={3} gap={2}>
  **Step 04 学习目标**
  - 理解 `appsettings.json` 和环境配置。
  - 理解 `IConfiguration`。
  - 掌握强类型配置绑定。
  - 掌握 `IOptions<T>`、`IOptionsSnapshot<T>`、`IOptionsMonitor<T>`。
  - 学会通过 DI 注入配置。
  - 为下一阶段 PostgreSQL + EF Core 连接配置做准备。
</box>

## 一、先理解 ASP.NET Core 配置体系

假设我们的订单系统需要配置：

- PostgreSQL 数据库连接字符串
- Redis 地址
- JWT 密钥
- 订单业务参数

传统方式可能会这样写：

```csharp
var connectionString = "Host=localhost;Port=5432;Database=orderdb;...";
```

这存在明显问题：

1. 修改数据库地址需要修改代码。
2. 开发、测试、生产环境配置不同。
3. 密码等敏感信息不应该提交到 Git。
4. 配置项越来越多，难以维护。

ASP.NET Core 提供统一配置系统。

### 1. 配置来源

常见配置来源包括：

| 配置来源 | 典型用途 | 优先级 |
|---|---|---|
| appsettings.json | 通用默认配置 | 低 |
| appsettings.Development.json | 本地开发配置 | 较高 |
| 环境变量 | Docker、服务器部署 | 更高 |
| 命令行参数 | 启动时临时覆盖 | 更高 |
| Secret Manager | 本地开发敏感信息 | 取决于注册顺序 |

默认情况下，ASP.NET Core WebApplication 会注册多个配置提供程序，后注册的配置通常覆盖先注册的同名配置。

例如：

```text
appsettings.json
       |
       v
appsettings.Development.json
       |
       v
Environment Variables
       |
       v
Command Line
```

这里要记住：**配置系统不是只读取 appsettings.json，而是将多个来源合并为一个 IConfiguration。**

## 二、创建业务配置文件

我们先不配置真实数据库，先模拟一个订单系统的业务配置。

打开 `appsettings.json`，添加：

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",

  "OrderSettings": {
    "SystemName": "OrderManagement",
    "MaxItemsPerOrder": 50,
    "EnableAutoCancel": true,
    "AutoCancelMinutes": 30
  }
}
```

这里新增了一个配置节点：

```json
"OrderSettings": {
  "SystemName": "OrderManagement",
  "MaxItemsPerOrder": 50,
  "EnableAutoCancel": true,
  "AutoCancelMinutes": 30
}
```

这几个配置项以后会用于订单业务：

| 配置项 | 类型 | 作用 |
|---|---|---|
| SystemName | string | 系统名称 |
| MaxItemsPerOrder | int | 单笔订单最大商品数量 |
| EnableAutoCancel | bool | 是否自动取消订单 |
| AutoCancelMinutes | int | 自动取消等待时间 |

## 三、创建强类型配置类

如果直接使用 `IConfiguration`，读取配置通常是这样的：

```csharp
var systemName = configuration["OrderSettings:SystemName"];

var maxItems = configuration["OrderSettings:MaxItemsPerOrder"];
```

存在两个问题：

- 配置项名称都是字符串，容易写错。
- 读取出来的值通常需要自己转换类型。

企业项目更推荐使用 **Options Pattern（选项模式）**。

在项目中创建：

```text
OrderManagement.Api/
└── Settings/
    └── OrderSettings.cs
```

`OrderSettings.cs`：

```csharp
namespace OrderManagement.Api.Settings;

public class OrderSettings
{
    public string SystemName { get; set; } = string.Empty;

    public int MaxItemsPerOrder { get; set; }

    public bool EnableAutoCancel { get; set; }

    public int AutoCancelMinutes { get; set; }
}
```

这里没有什么特殊语法，就是一个普通的 C# POCO 类。

ASP.NET Core 会将 JSON 配置绑定到这个对象。

例如：

```text
OrderSettings:SystemName
             |
             v
OrderSettings.SystemName
```

这就是 Configuration Binding。

## 四、在 Program.cs 注册 Options

打开 `Program.cs`。

引入：

```csharp
using OrderManagement.Api.Settings;
```

在 `builder.Build()` 之前添加：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

现在你的 Program.cs 核心结构应该类似：

```csharp
using OrderManagement.Api.Services;
using OrderManagement.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// DI 生命周期实验
builder.Services.AddTransient<ITransientOperationService, OperationService>();
builder.Services.AddScoped<IScopedOperationService, OperationService>();
builder.Services.AddSingleton<ISingletonOperationService, OperationService>();

// 注册订单配置
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

这里重点理解：

```csharp
builder.Configuration.GetSection("OrderSettings")
```

它表示从整个配置系统中获取 `OrderSettings` 这个配置节点。

而：

```csharp
builder.Services.Configure<OrderSettings>(...)
```

则是把配置绑定到 `OrderSettings` 类型，并注册 Options 服务。

不需要自己 `new OrderSettings()`，也不需要手动解析每个字段。

## 五、创建 SettingsController 测试配置

现在我们通过 Controller 验证配置是否正确。

创建：

`Controllers/SettingsController.cs`

这次我给你完整代码：

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly OrderSettings _settings;

    public SettingsController(IOptions<OrderSettings> options)
    {
        _settings = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            SystemName = _settings.SystemName,
            MaxItemsPerOrder = _settings.MaxItemsPerOrder,
            EnableAutoCancel = _settings.EnableAutoCancel,
            AutoCancelMinutes = _settings.AutoCancelMinutes
        });
    }
}
```

运行项目：

```bash
dotnet run
```

访问：

```http
GET /api/settings
```

预期返回：

```json
{
  "systemName": "OrderManagement",
  "maxItemsPerOrder": 50,
  "enableAutoCancel": true,
  "autoCancelMinutes": 30
}
```

<badge color="success">阶段成果</badge> 到这里，我们已经完成了从 JSON 配置文件到 C# 强类型对象，再到 Controller 注入的完整过程。

---

## 六、深入理解三种 Options

这是本阶段最重要的理论知识，也是面试经常考察的内容。

ASP.NET Core 提供三种常用 Options 接口。

| 类型 | 生命周期 | 配置变化后的行为 |
|---|---|---|
| IOptions<T> | 可在任意生命周期服务中注入 | 不会自动更新已读取的 Value |
| IOptionsSnapshot<T> | Scoped | 下一个 Scope 获取新配置快照 |
| IOptionsMonitor<T> | Singleton | 支持获取最新配置和变化通知 |

注意：这里说的是 Options 对象的行为，不是说它们分别等同于 DI 的三种生命周期。

### 1. IOptions<T>

```csharp
public SettingsController(IOptions<OrderSettings> options)
{
    _settings = options.Value;
}
```

特点：

- 使用简单。
- 适合普通配置读取。
- 不适合需要实时响应配置变化的场景。

例如：系统名称、默认分页数量等。

### 2. IOptionsSnapshot<T>

```csharp
public SettingsController(
    IOptionsSnapshot<OrderSettings> options)
{
    _settings = options.Value;
}
```

特点：

- Scoped 生命周期。
- 每个 Scope 获取配置快照。
- 配置文件发生变化后，后续 Scope 可以获取新值。

适合需要按请求读取最新配置的业务服务。

注意：不能将 `IOptionsSnapshot<T>` 直接注入 Singleton 服务。

### 3. IOptionsMonitor<T>

```csharp
public class OrderService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderService(IOptionsMonitor<OrderSettings> options)
    {
        _options = options;
    }

    public int GetMaxItems()
    {
        return _options.CurrentValue.MaxItemsPerOrder;
    }
}
```

特点：

- Singleton 生命周期。
- 支持读取当前配置。
- 支持配置变化通知。
- 可以通过 `OnChange()` 监听变化。

例如：

```csharp
_options.OnChange(settings =>
{
    Console.WriteLine(
        $"订单最大商品数量已调整：{settings.MaxItemsPerOrder}");
});
```

真实企业项目中，如果需要动态调整某些运行参数，而不重启服务，`IOptionsMonitor` 就很有用。

不过，配置源必须支持重新加载；例如默认 JSON 配置提供程序支持文件变化重新加载。

## 七、做一个小实验：验证配置变化

现在我们做一个很实用的实验。

1. 运行 API。
2. 访问 `/api/settings`。
3. 修改 `appsettings.json`：

```json
"MaxItemsPerOrder": 100
```

4. 保存文件。
5. 再次访问 `/api/settings`。

观察返回值是否变成 100。

因为默认 JSON 配置提供程序支持文件变更重新加载，但 `IOptions<T>` 的 `Value` 不会自动更新，因此你可能会发现它仍然是原来的 50。

这正是我们后面使用 `IOptionsSnapshot` 和 `IOptionsMonitor` 的原因。

建议你进一步把 Controller 改成注入 `IOptionsSnapshot<OrderSettings>`，再测试一次。然后尝试 `IOptionsMonitor<OrderSettings>`，对比三者的行为。

---

## 八、再补充一个企业项目常用知识：环境变量覆盖配置

例如 Docker 部署时，我们通常不希望修改 appsettings.json。

可以通过环境变量覆盖：

```bash
export OrderSettings__MaxItemsPerOrder=200
```

ASP.NET Core 会将双下划线 `__` 转换为配置层级分隔符 `:`。

等价于：

```text
OrderSettings:MaxItemsPerOrder
```

因此环境变量的值会覆盖 JSON 中的 50。

敏感信息，例如数据库密码、JWT Secret，不应该直接写在提交到 Git 的 appsettings.json 中。开发阶段可以使用 User Secrets，部署环境则可以使用环境变量或密钥管理服务。

---

## 九、Step 04 阶段验收

{@body const items = ["创建 OrderSettings.cs","修改 appsettings.json，增加 OrderSettings 配置","在 Program.cs 注册 Configure<OrderSettings>","创建 SettingsController 并成功返回配置","修改 JSON，验证配置重新加载","理解 IOptions、IOptionsSnapshot、IOptionsMonitor 的区别"]}
{@body const [checked,setChecked] = DIL.useState([])}
<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Step 04 任务清单**
    <badge color={checked.length===items.length?"success":"secondary"}>{checked.length}/{items.length}</badge>
  </row>
  <box background="surface-tertiary" height="6px" radius="full" clip>
    <box background="#16A34A" height="6px" radius="full" width={`${checked.length/items.length*100}%`}/>
  </box>
  {#each items as item,i}
    <checkbox key={i} checked={checked.includes(i)} onChange={v=>setChecked(old=>v?[...old,i]:old.filter(x=>x!==i))}>{item}</checkbox>
  {/each}
  <button block disabled={checked.length!==items.length} onClick={()=>GenUI.issueNewTurn("Step 04 已完成，请检查并进入 Step 05：EF Core + PostgreSQL。")}>提交阶段验收</button>
</box>

## 十、面试知识题

最后，尝试回答这三个问题，不需要背标准答案：

1. `IConfiguration` 和 `IOptions<T>` 有什么区别？
2. `IOptionsSnapshot<T>` 为什么不能注入 Singleton 服务？
3. 如果生产环境需要动态修改订单自动取消时间，而且不重启 API，应该选择哪种 Options？

**下一阶段 Step 05：EF Core 10 + PostgreSQL。**

我们将开始真正连接数据库，学习 Entity、DbContext、DbSet、Code First、Migration，并完成第一个持久化 CRUD。

这一步开始，OrderManagement 就不再只是内存中的 Demo，而是真正拥有数据库的后端项目。

---

### 

## 二、创建业务配置文件

我们先不配置真实数据库，先模拟一个订单系统的业务配置。

打开 `appsettings.json`，添加：

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",

  "OrderSettings": {
    "SystemName": "OrderManagement",
    "MaxItemsPerOrder": 50,
    "EnableAutoCancel": true,
    "AutoCancelMinutes": 30
  }
}
```

这里新增了一个配置节点：

```json
"OrderSettings": {
  "SystemName": "OrderManagement",
  "MaxItemsPerOrder": 50,
  "EnableAutoCancel": true,
  "AutoCancelMinutes": 30
}
```

这几个配置项以后会用于订单业务：

| 配置项 | 类型 | 作用 |
|---|---|---|
| SystemName | string | 系统名称 |
| MaxItemsPerOrder | int | 单笔订单最大商品数量 |
| EnableAutoCancel | bool | 是否自动取消订单 |
| AutoCancelMinutes | int | 自动取消等待时间 |

## 三、创建强类型配置类

如果直接使用 `IConfiguration`，读取配置通常是这样的：

```csharp
var systemName = configuration["OrderSettings:SystemName"];

var maxItems = configuration["OrderSettings:MaxItemsPerOrder"];
```

存在两个问题：

- 配置项名称都是字符串，容易写错。
- 读取出来的值通常需要自己转换类型。

企业项目更推荐使用 **Options Pattern（选项模式）**。

在项目中创建：

```text
OrderManagement.Api/
└── Settings/
    └── OrderSettings.cs
```

`OrderSettings.cs`：

```csharp
namespace OrderManagement.Api.Settings;

public class OrderSettings
{
    public string SystemName { get; set; } = string.Empty;

    public int MaxItemsPerOrder { get; set; }

    public bool EnableAutoCancel { get; set; }

    public int AutoCancelMinutes { get; set; }
}
```

这里没有什么特殊语法，就是一个普通的 C# POCO 类。

ASP.NET Core 会将 JSON 配置绑定到这个对象。

例如：

```text
OrderSettings:SystemName
             |
             v
OrderSettings.SystemName
```

这就是 Configuration Binding。

## 四、在 Program.cs 注册 Options

打开 `Program.cs`。

引入：

```csharp
using OrderManagement.Api.Settings;
```

在 `builder.Build()` 之前添加：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

现在你的 Program.cs 核心结构应该类似：

```csharp
using OrderManagement.Api.Services;
using OrderManagement.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// DI 生命周期实验
builder.Services.AddTransient<ITransientOperationService, OperationService>();
builder.Services.AddScoped<IScopedOperationService, OperationService>();
builder.Services.AddSingleton<ISingletonOperationService, OperationService>();

// 注册订单配置
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

这里重点理解：

```csharp
builder.Configuration.GetSection("OrderSettings")
```

它表示从整个配置系统中获取 `OrderSettings` 这个配置节点。

而：

```csharp
builder.Services.Configure<OrderSettings>(...)
```

则是把配置绑定到 `OrderSettings` 类型，并注册 Options 服务。

不需要自己 `new OrderSettings()`，也不需要手动解析每个字段。

## 五、创建 SettingsController 测试配置

现在我们通过 Controller 验证配置是否正确。

创建：

`Controllers/SettingsController.cs`

这次我给你完整代码：

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly OrderSettings _settings;

    public SettingsController(IOptions<OrderSettings> options)
    {
        _settings = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            SystemName = _settings.SystemName,
            MaxItemsPerOrder = _settings.MaxItemsPerOrder,
            EnableAutoCancel = _settings.EnableAutoCancel,
            AutoCancelMinutes = _settings.AutoCancelMinutes
        });
    }
}
```

运行项目：

```bash
dotnet run
```

访问：

```http
GET /api/settings
```

预期返回：

```json
{
  "systemName": "OrderManagement",
  "maxItemsPerOrder": 50,
  "enableAutoCancel": true,
  "autoCancelMinutes": 30
}
```

<badge color="success">阶段成果</badge> 到这里，我们已经完成了从 JSON 配置文件到 C# 强类型对象，再到 Controller 注入的完整过程。

---

## 六、深入理解三种 Options

这是本阶段最重要的理论知识，也是面试经常考察的内容。

ASP.NET Core 提供三种常用 Options 接口。

| 类型 | 生命周期 | 配置变化后的行为 |
|---|---|---|
| IOptions<T> | 可在任意生命周期服务中注入 | 不会自动更新已读取的 Value |
| IOptionsSnapshot<T> | Scoped | 下一个 Scope 获取新配置快照 |
| IOptionsMonitor<T> | Singleton | 支持获取最新配置和变化通知 |

注意：这里说的是 Options 对象的行为，不是说它们分别等同于 DI 的三种生命周期。

### 1. IOptions<T>

```csharp
public SettingsController(IOptions<OrderSettings> options)
{
    _settings = options.Value;
}
```

特点：

- 使用简单。
- 适合普通配置读取。
- 不适合需要实时响应配置变化的场景。

例如：系统名称、默认分页数量等。

### 2. IOptionsSnapshot<T>

```csharp
public SettingsController(
    IOptionsSnapshot<OrderSettings> options)
{
    _settings = options.Value;
}
```

特点：

- Scoped 生命周期。
- 每个 Scope 获取配置快照。
- 配置文件发生变化后，后续 Scope 可以获取新值。

适合需要按请求读取最新配置的业务服务。

注意：不能将 `IOptionsSnapshot<T>` 直接注入 Singleton 服务。

### 3. IOptionsMonitor<T>

```csharp
public class OrderService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderService(IOptionsMonitor<OrderSettings> options)
    {
        _options = options;
    }

    public int GetMaxItems()
    {
        return _options.CurrentValue.MaxItemsPerOrder;
    }
}
```

特点：

- Singleton 生命周期。
- 支持读取当前配置。
- 支持配置变化通知。
- 可以通过 `OnChange()` 监听变化。

例如：

```csharp
_options.OnChange(settings =>
{
    Console.WriteLine(
        $"订单最大商品数量已调整：{settings.MaxItemsPerOrder}");
});
```

真实企业项目中，如果需要动态调整某些运行参数，而不重启服务，`IOptionsMonitor` 就很有用。

不过，配置源必须支持重新加载；例如默认 JSON 配置提供程序支持文件变化重新加载。

## 七、做一个小实验：验证配置变化

现在我们做一个很实用的实验。

1. 运行 API。
2. 访问 `/api/settings`。
3. 修改 `appsettings.json`：

```json
"MaxItemsPerOrder": 100
```

4. 保存文件。
5. 再次访问 `/api/settings`。

观察返回值是否变成 100。

因为默认 JSON 配置提供程序支持文件变更重新加载，但 `IOptions<T>` 的 `Value` 不会自动更新，因此你可能会发现它仍然是原来的 50。

这正是我们后面使用 `IOptionsSnapshot` 和 `IOptionsMonitor` 的原因。

建议你进一步把 Controller 改成注入 `IOptionsSnapshot<OrderSettings>`，再测试一次。然后尝试 `IOptionsMonitor<OrderSettings>`，对比三者的行为。

---

## 八、再补充一个企业项目常用知识：环境变量覆盖配置

例如 Docker 部署时，我们通常不希望修改 appsettings.json。

可以通过环境变量覆盖：

```bash
export OrderSettings__MaxItemsPerOrder=200
```

ASP.NET Core 会将双下划线 `__` 转换为配置层级分隔符 `:`。

等价于：

```text
OrderSettings:MaxItemsPerOrder
```

因此环境变量的值会覆盖 JSON 中的 50。

敏感信息，例如数据库密码、JWT Secret，不应该直接写在提交到 Git 的 appsettings.json 中。开发阶段可以使用 User Secrets，部署环境则可以使用环境变量或密钥管理服务。

---

## 九、Step 04 阶段验收

{@body const items = ["创建 OrderSettings.cs","修改 appsettings.json，增加 OrderSettings 配置","在 Program.cs 注册 Configure<OrderSettings>","创建 SettingsController 并成功返回配置","修改 JSON，验证配置重新加载","理解 IOptions、IOptionsSnapshot、IOptionsMonitor 的区别"]}
{@body const [checked,setChecked] = DIL.useState([])}
<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Step 04 任务清单**
    <badge color={checked.length===items.length?"success":"secondary"}>{checked.length}/{items.length}</badge>
  </row>
  <box background="surface-tertiary" height="6px" radius="full" clip>
    <box background="#16A34A" height="6px" radius="full" width={`${checked.length/items.length*100}%`}/>
  </box>
  {#each items as item,i}
    <checkbox key={i} checked={checked.includes(i)} onChange={v=>setChecked(old=>v?[...old,i]:old.filter(x=>x!==i))}>{item}</checkbox>
  {/each}
  <button block disabled={checked.length!==items.length} onClick={()=>GenUI.issueNewTurn("Step 04 已完成，请检查并进入 Step 05：EF Core + PostgreSQL。")}>提交阶段验收</button>
</box>

## 十、面试知识题

最后，尝试回答这三个问题，不需要背标准答案：

1. `IConfiguration` 和 `IOptions<T>` 有什么区别？
2. `IOptionsSnapshot<T>` 为什么不能注入 Singleton 服务？
3. 如果生产环境需要动态修改订单自动取消时间，而且不重启 API，应该选择哪种 Options？

**下一阶段 Step 05：EF Core 10 + PostgreSQL。**

我们将开始真正连接数据库，学习 Entity、DbContext、DbSet、Code First、Migration，并完成第一个持久化 CRUD。

这一步开始，OrderManagement 就不再只是内存中的 Demo，而是真正拥有数据库的后端项目。

---

### 

继续吧，深入理解三种 Options 这部分，内容好像没有输出完

---

### 

你说得对。上一节虽然介绍了 `IOptions<T>`、`IOptionsSnapshot<T>`、`IOptionsMonitor<T>`，但主要停留在概念和简单代码层面，还没有真正把它们的**生命周期、配置缓存、配置热更新、线程安全以及实际使用场景**讲透。

这部分值得单独深入，因为它既是 ASP.NET Core 的基础设施知识，也是 .NET 后端面试中的常见问题。

我们暂时不进入 EF Core，先把 Step 04 的 Options Pattern 补完整。

# Step 04 补充：深入理解三种 Options

## 一、先理解 Options Pattern 的整体工作原理

我们先看一下完整链路：

<box border radius="lg" padding={3} gap={2}>
  <box background="rgba(59,130,246,0.09)" radius="md" padding={3} align="center">
    **appsettings.json**
    <text color="secondary" size="xs">配置文件</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="secondary" size="lg"/>
  </box>
  <box background="rgba(59,130,246,0.09)" radius="md" padding={3} align="center">
    **IConfiguration**
    <text color="secondary" size="xs">统一配置系统，合并 JSON、环境变量等来源</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="secondary" size="lg"/>
  </box>
  <box background="rgba(59,130,246,0.09)" radius="md" padding={3} align="center">
    **Configuration Binder**
    <text color="secondary" size="xs">将配置绑定到 C# 对象</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="secondary" size="lg"/>
  </box>
  <box background="rgba(22,163,74,0.09)" radius="md" padding={3} align="center">
    **OrderSettings**
    <text color="secondary" size="xs">强类型配置对象</text>
  </box>
  <divider color="subtle"/>
  **通过 DI 注入三种 Options 接口**
  <grid columns={3} gap={2}>
    <grid-item>
      <box border radius="md" padding={2} align="center" gap={1}>
        **IOptions**
        <text color="secondary" size="xs">稳定配置</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={2} align="center" gap={1}>
        **Snapshot**
        <text color="secondary" size="xs">请求快照</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={2} align="center" gap={1}>
        **Monitor**
        <text color="secondary" size="xs">动态监控</text>
      </box>
    </grid-item>
  </grid>
</box>

首先明确一个概念：

`IOptions<T>`、`IOptionsSnapshot<T>`、`IOptionsMonitor<T>` 并不是三种不同的配置文件读取方式。

它们是三种不同的**配置访问与缓存策略**。

三者最终都可以获得 `OrderSettings` 对象，但获取对象的时机、缓存方式和更新行为不同。

## 二、IOptions<T>：全局缓存配置

### 1. 基本代码

```csharp
public class OrderService
{
    private readonly OrderSettings _settings;

    public OrderService(IOptions<OrderSettings> options)
    {
        _settings = options.Value;
    }

    public int GetMaxItems()
    {
        return _settings.MaxItemsPerOrder;
    }
}
```

注册方式：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));

builder.Services.AddScoped<OrderService>();
```

### 2. 它的生命周期是什么？

`IOptions<T>` 本身可以注入 Singleton、Scoped、Transient 服务。

它不会因为注入的 Service 生命周期不同，就自动产生不同配置快照。

默认实现 `OptionsManager<T>` 使用缓存机制，同一个 Options 类型及名称通常只创建一次配置实例。

例如：

```text
应用启动
   |
   v
第一次请求 OrderService
   |
   v
获取 IOptions<OrderSettings>
   |
   v
首次访问 Value
   |
   v
创建并缓存 OrderSettings
   |
   v
后续请求继续使用缓存值
```

### 3. 修改配置文件会怎样？

假设最初：

```json
"OrderSettings": {
  "MaxItemsPerOrder": 50
}
```

应用运行后，第一次读取，得到：

```text
MaxItemsPerOrder = 50
```

现在修改为：

```json
"MaxItemsPerOrder": 100
```

默认 JSON Provider 会重新加载配置，但之前已经缓存的 `IOptions<T>.Value` 不会自动重新创建。

因此，继续读取：

```text
MaxItemsPerOrder = 50
```

需要注意：如果第一次读取发生在修改之后，首次缓存的就可能是 100。这里讨论的是已经创建并缓存之后再修改配置的情况。

### 4. 什么时候使用？

适合：

- 系统基本信息
- 不经常变化的业务配置
- 固定的默认参数
- 不需要运行时热更新的配置

例如：

```csharp
public class OrderService
{
    private readonly OrderSettings _settings;

    public OrderService(IOptions<OrderSettings> options)
    {
        _settings = options.Value;
    }
}
```

如果订单最大商品数量在运行期间不会动态修改，使用它完全足够。



---

### 

你说得对。上一节虽然介绍了 `IOptions<T>`、`IOptionsSnapshot<T>`、`IOptionsMonitor<T>`，但主要停留在概念和简单代码层面，还没有真正把它们的**生命周期、配置缓存、配置热更新、线程安全以及实际使用场景**讲透。

这部分值得单独深入，因为它既是 ASP.NET Core 的基础设施知识，也是 .NET 后端面试中的常见问题。

我们暂时不进入 EF Core，先把 Step 04 的 Options Pattern 补完整。

# Step 04 补充：深入理解三种 Options

## 一、先理解 Options Pattern 的整体工作原理

我们先看一下完整链路：

<box border radius="lg" padding={3} gap={2}>
  <box background="rgba(59,130,246,0.09)" radius="md" padding={3} align="center">
    **appsettings.json**
    <text color="secondary" size="xs">配置文件</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="secondary" size="lg"/>
  </box>
  <box background="rgba(59,130,246,0.09)" radius="md" padding={3} align="center">
    **IConfiguration**
    <text color="secondary" size="xs">统一配置系统，合并 JSON、环境变量等来源</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="secondary" size="lg"/>
  </box>
  <box background="rgba(59,130,246,0.09)" radius="md" padding={3} align="center">
    **Configuration Binder**
    <text color="secondary" size="xs">将配置绑定到 C# 对象</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="secondary" size="lg"/>
  </box>
  <box background="rgba(22,163,74,0.09)" radius="md" padding={3} align="center">
    **OrderSettings**
    <text color="secondary" size="xs">强类型配置对象</text>
  </box>
  <divider color="subtle"/>
  **通过 DI 注入三种 Options 接口**
  <grid columns={3} gap={2}>
    <grid-item>
      <box border radius="md" padding={2} align="center" gap={1}>
        **IOptions**
        <text color="secondary" size="xs">稳定配置</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={2} align="center" gap={1}>
        **Snapshot**
        <text color="secondary" size="xs">请求快照</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={2} align="center" gap={1}>
        **Monitor**
        <text color="secondary" size="xs">动态监控</text>
      </box>
    </grid-item>
  </grid>
</box>

首先明确一个概念：

`IOptions<T>`、`IOptionsSnapshot<T>`、`IOptionsMonitor<T>` 并不是三种不同的配置文件读取方式。

它们是三种不同的**配置访问与缓存策略**。

三者最终都可以获得 `OrderSettings` 对象，但获取对象的时机、缓存方式和更新行为不同。

## 二、IOptions<T>：全局缓存配置

### 1. 基本代码

```csharp
public class OrderService
{
    private readonly OrderSettings _settings;

    public OrderService(IOptions<OrderSettings> options)
    {
        _settings = options.Value;
    }

    public int GetMaxItems()
    {
        return _settings.MaxItemsPerOrder;
    }
}
```

注册方式：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));

builder.Services.AddScoped<OrderService>();
```

### 2. 它的生命周期是什么？

`IOptions<T>` 本身可以注入 Singleton、Scoped、Transient 服务。

它不会因为注入的 Service 生命周期不同，就自动产生不同配置快照。

默认实现 `OptionsManager<T>` 使用缓存机制，同一个 Options 类型及名称通常只创建一次配置实例。

例如：

```text
应用启动
   |
   v
第一次请求 OrderService
   |
   v
获取 IOptions<OrderSettings>
   |
   v
首次访问 Value
   |
   v
创建并缓存 OrderSettings
   |
   v
后续请求继续使用缓存值
```

### 3. 修改配置文件会怎样？

假设最初：

```json
"OrderSettings": {
  "MaxItemsPerOrder": 50
}
```

应用运行后，第一次读取，得到：

```text
MaxItemsPerOrder = 50
```

现在修改为：

```json
"MaxItemsPerOrder": 100
```

默认 JSON Provider 会重新加载配置，但之前已经缓存的 `IOptions<T>.Value` 不会自动重新创建。

因此，继续读取：

```text
MaxItemsPerOrder = 50
```

需要注意：如果第一次读取发生在修改之后，首次缓存的就可能是 100。这里讨论的是已经创建并缓存之后再修改配置的情况。

### 4. 什么时候使用？

适合：

- 系统基本信息
- 不经常变化的业务配置
- 固定的默认参数
- 不需要运行时热更新的配置

例如：

```csharp
public class OrderService
{
    private readonly OrderSettings _settings;

    public OrderService(IOptions<OrderSettings> options)
    {
        _settings = options.Value;
    }
}
```

如果订单最大商品数量在运行期间不会动态修改，使用它完全足够。

## 三、IOptionsSnapshot<T>：每个 Scope 获取配置快照

这是三种 Options 中最容易被误解的一个。

### 1. 基本代码

将原来的 Controller 改成：

```csharp
[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly OrderSettings _settings;

    public SettingsController(
        IOptionsSnapshot<OrderSettings> options)
    {
        _settings = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            _settings.SystemName,
            _settings.MaxItemsPerOrder,
            _settings.EnableAutoCancel,
            _settings.AutoCancelMinutes
        });
    }
}
```

注意：这里只是将 `IOptions<OrderSettings>` 换成了 `IOptionsSnapshot<OrderSettings>`。

### 2. 它是如何工作的？

`IOptionsSnapshot<T>` 的特点：

- Scoped 生命周期。
- 每个 Scope 有自己的 Options 缓存。
- 同一个 Scope 内读取同一个配置名称，复用同一个配置对象。
- 新 Scope 可以根据当前配置重新创建配置对象。

在 ASP.NET Core Web API 中，通常一次 HTTP 请求对应一个 Scope。

我们来看一个例子。

初始配置：

```json
"MaxItemsPerOrder": 50
```

请求过程：

<box border radius="lg" padding={3} gap={2}>
  **HTTP Request 1**
  <box background="rgba(59,130,246,0.08)" padding={3} radius="md" gap={1}>
    <text weight="medium">Scope A</text>
    <row justify="between">
      <text color="secondary" size="sm">第一次读取</text>
      **50**
    </row>
    <row justify="between">
      <text color="secondary" size="sm">第二次读取</text>
      **50**
    </row>
    <caption>同一个 Scope 内使用缓存</caption>
  </box>
  <divider color="subtle"/>
  <text color="secondary" size="sm">修改 appsettings.json：50 → 100</text>
  <divider color="subtle"/>
  **HTTP Request 2**
  <box background="rgba(22,163,74,0.08)" padding={3} radius="md" gap={1}>
    <text weight="medium">Scope B</text>
    <row justify="between">
      <text color="secondary" size="sm">第一次读取</text>
      **100**
    </row>
    <row justify="between">
      <text color="secondary" size="sm">第二次读取</text>
      **100**
    </row>
    <caption>新 Scope 根据当前配置创建快照</caption>
  </box>
</box>

这里有一个重要细节：

**Snapshot 并不是每次访问 Value 都重新读取 JSON。**

它是在当前 Scope 第一次读取对应配置时创建并缓存对象。

所以同一个请求中，即使配置发生变化，已经创建的快照通常也不会改变。

### 3. 为什么不能注入 Singleton？

假设：

```csharp
public class OrderBackgroundService : BackgroundService
{
    private readonly IOptionsSnapshot<OrderSettings> _options;

    public OrderBackgroundService(
        IOptionsSnapshot<OrderSettings> options)
    {
        _options = options;
    }

    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
```

这段代码会出现 DI 生命周期问题。

原因：

- `BackgroundService` 是 Singleton。
- `IOptionsSnapshot<T>` 是 Scoped。
- Singleton 的生命周期比 Scoped 长。

在启用 Scope 验证时，会报类似这样的错误：

```text
Cannot consume scoped service
'IOptionsSnapshot<OrderSettings>'
from singleton
'OrderBackgroundService'.
```

这与之前学习的 Singleton 不能直接依赖 Scoped 是同一个问题。

### 4. 什么时候使用？

例如：

- 每个 HTTP 请求读取一份配置快照。
- 某些业务参数可能动态调整。
- 不要求同一个请求处理中实时切换配置。

企业 Web API 中，如果配置需要在后续请求中更新，`IOptionsSnapshot<T>` 是很自然的选择。

---

## 四、IOptionsMonitor<T>：动态配置监控

这是三种 Options 中功能最丰富的一种。

它特别适合 Singleton 服务和后台服务。

### 1. 基本代码

创建一个服务：

`Services/OrderMonitorService.cs`

```csharp
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Services;

public class OrderMonitorService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderMonitorService(
        IOptionsMonitor<OrderSettings> options)
    {
        _options = options;

        // 监听配置变化
        _options.OnChange(settings =>
        {
            Console.WriteLine(
                $"订单配置发生变化，最大商品数量：{settings.MaxItemsPerOrder}");
        });
    }

    public int GetMaxItems()
    {
        return _options.CurrentValue.MaxItemsPerOrder;
    }
}
```

在 `Program.cs` 注册：

```csharp
builder.Services.AddSingleton<OrderMonitorService>();
```

注意这里使用 Singleton。

因为 `IOptionsMonitor<T>` 本身就是 Singleton 生命周期，可以安全地注入 Singleton 服务。

### 2. CurrentValue 是什么？

```csharp
_options.CurrentValue.MaxItemsPerOrder
```

每次访问 `CurrentValue`，都会获取当前缓存的配置对象。

配置发生变化后，Options 系统会监听配置变更令牌，清除相应的缓存。

下一次访问 `CurrentValue` 时，会重新创建配置对象。

例如：

```text
应用启动
   |
   v
CurrentValue = 50
   |
   v
修改配置文件：50 → 100
   |
   v
配置系统触发变更通知
   |
   v
清除对应 Options 缓存
   |
   v
CurrentValue = 100
```

### 3. OnChange() 是什么？

```csharp
_options.OnChange(settings =>
{
    Console.WriteLine(
        $"配置已更新：{settings.MaxItemsPerOrder}");
});
```

它可以让程序主动感知配置变化。

例如：

- 修改订单最大商品数量。
- 修改某个业务开关。
- 修改第三方服务的连接参数。
- 动态调整某些后台任务参数。

需要注意，`OnChange()` 返回一个 `IDisposable`，用于取消监听。

如果长期运行的服务注册监听，建议保存这个对象，并在服务释放时正确 Dispose，避免重复订阅。

### 4. 为什么 Monitor 可以用于 Singleton？

因为：

```text
OrderMonitorService (Singleton)
           |
           v
IOptionsMonitor (Singleton)
           |
           v
OrderSettings 当前配置
```

它不会持有一个固定的 Scoped 配置快照。

而是通过内部缓存管理和配置变更通知机制，提供当前配置。

因此它非常适合后台任务：

```csharp
public class OrderBackgroundService : BackgroundService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderBackgroundService(
        IOptionsMonitor<OrderSettings> options)
    {
        _options = options;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var minutes = _options.CurrentValue.AutoCancelMinutes;

            Console.WriteLine($"当前自动取消时间：{minutes}");

            await Task.Delay(
                TimeSpan.FromMinutes(1),
                stoppingToken);
        }
    }
}
```

这里每次循环都会读取最新配置，而不是在构造函数中提前保存一个旧值。

不过要注意：如果修改的是正在执行中的任务参数，已经开始执行的那次业务操作是否切换到新参数，需要由业务代码明确决定。

## 五、三种 Options 核心区别对比

现在把三个接口放在一起比较。

| 对比项 | IOptions | IOptionsSnapshot | IOptionsMonitor |
|---|---|---|---|
| DI 生命周期 | Singleton | Scoped | Singleton |
| 配置缓存 | 全局缓存 | 每个 Scope 缓存 | 全局缓存，可失效重建 |
| 同一请求内 | 不变 | 不变 | 可随更新变化 |
| 后续请求 | 使用原缓存 | 可获取新配置 | 可获取新配置 |
| 支持热更新 | 否 | 支持新 Scope 获取 | 支持 |
| OnChange | 不支持 | 不支持 | 支持 |
| 注入 Singleton | 可以 | 不可以 | 可以 |
| 典型用途 | 固定配置 | 请求级快照 | 动态配置、后台服务 |

特别强调两个区别：

**Snapshot 和 Monitor 都能获取更新后的配置，但机制不同。**

- Snapshot：新 Scope 获取新配置。
- Monitor：同一个服务实例也能获取更新后的配置。

这也是面试中很值得说明的地方。

## 六、配置热更新的前提条件

前面我们一直在讲修改 `appsettings.json`，但有一个前提：

配置源必须支持重新加载。

默认 ASP.NET Core WebApplication 的 JSON 配置提供程序通常启用了：

```csharp
reloadOnChange: true
```

可以显式理解为：

```csharp
builder.Configuration.AddJsonFile(
    "appsettings.json",
    optional: false,
    reloadOnChange: true);
```

但要注意，这段代码是额外添加配置提供程序的示例，不能直接不加判断地重复添加到默认配置链中。默认情况下已经注册了 JSON 配置文件。

如果配置来源是普通环境变量，修改操作通常需要重新启动进程，不能简单认为环境变量也会自动热更新。

另外，生产环境使用容器部署时，文件挂载、文件系统通知等也会影响配置变更能否被检测到。

## 七、企业项目中还有一个重要知识点：Options 验证

假设我们配置：

```json
"OrderSettings": {
  "SystemName": "",
  "MaxItemsPerOrder": -1,
  "EnableAutoCancel": true,
  "AutoCancelMinutes": 0
}
```

这些值明显不合理。

如果没有验证，程序可能正常启动，但运行到订单业务时才出现问题。

ASP.NET Core 支持配置验证。

修改 `OrderSettings.cs`：

```csharp
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Api.Settings;

public class OrderSettings
{
    [Required]
    public string SystemName { get; set; } = string.Empty;

    [Range(1, 100)]
    public int MaxItemsPerOrder { get; set; }

    public bool EnableAutoCancel { get; set; }

    [Range(1, 1440)]
    public int AutoCancelMinutes { get; set; }
}
```

修改 Program.cs：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

这段代码做了三件事：

1. `Bind()`：将配置绑定到 OrderSettings。
2. `ValidateDataAnnotations()`：使用 DataAnnotations 验证配置。
3. `ValidateOnStart()`：应用启动时验证配置。

如果配置不符合要求，应用启动就会失败，而不是等到业务运行时才暴露问题。

这是非常推荐的企业开发实践。

---

## 八、动手实验：在我们的项目中真正验证三种 Options

理论部分已经完整了。现在我们做一个实验，把三个接口放在同一个 API 中观察。

### 实验目标

我们准备测试：

- IOptions：是否始终读取旧配置。
- IOptionsSnapshot：新请求是否读取新配置。
- IOptionsMonitor：当前服务实例是否读取新配置。

### 第一步：创建 OptionsExperimentController

这次你自己写，要求如下：

构造函数注入：

```csharp
IOptions<OrderSettings>
IOptionsSnapshot<OrderSettings>
IOptionsMonitor<OrderSettings>
```

创建 GET 接口：

```http
GET /api/options-experiment
```

返回：

```json
{
  "options": 50,
  "snapshot": 50,
  "monitor": 50
}
```

提示：

```csharp
_options.Value.MaxItemsPerOrder

_snapshot.Value.MaxItemsPerOrder

_monitor.CurrentValue.MaxItemsPerOrder
```

### 第二步：运行实验

先将 JSON 配置设为：

```json
"MaxItemsPerOrder": 50
```

访问接口，确认三个值都是 50。

然后修改配置：

```json
"MaxItemsPerOrder": 100
```

保存文件，再次访问。

预期结果：

```json
{
  "options": 50,
  "snapshot": 100,
  "monitor": 100
}
```

这就是三种 Options 最直观的区别。

### 第三步：验证 Monitor 的 OnChange

将之前的 `OrderMonitorService` 注册为 Singleton，并通过构造函数注册 `OnChange()`。

修改 JSON 后观察 Rider 的运行控制台，是否出现：

```text
订单配置发生变化，最大商品数量：100
```

这样就把配置文件、配置绑定、DI、缓存、热更新全部串联起来了。

<box border radius="lg" padding={3} gap={2}>
  **本次实验验收**
  {@body const tasks = ["理解 IOptions 的全局缓存行为","理解 IOptionsSnapshot 的 Scope 缓存行为","理解 IOptionsMonitor 的 CurrentValue 和 OnChange","创建 OptionsExperimentController","修改配置文件并验证三种结果","完成 Options 配置验证"]}
  {@body const [done,setDone] = DIL.useState([])}
  <row align="center" justify="between">
    <text color="secondary" size="xs">完成进度</text>
    <badge color={done.length===tasks.length?"success":"secondary"}>{done.length}/{tasks.length}</badge>
  </row>
  <box background="surface-tertiary" radius="full" height="6px" clip>
    <box background="#16A34A" height="6px" radius="full" width={`${done.length/tasks.length*100}%`}/>
  </box>
  {#each tasks as task,i}
    <checkbox key={i} checked={done.includes(i)} onChange={v=>setDone(old=>v?[...old,i]:old.filter(x=>x!==i))}>{task}</checkbox>
  {/each}
</box>

## 九、面试模拟：检验是否真正掌握

尝试回答下面 5 道题：

**Q1.** `IOptions<T>` 和 `IOptionsSnapshot<T>` 最大区别是什么？

**Q2.** 为什么 `IOptionsSnapshot<T>` 不能注入 Singleton 服务？

**Q3.** 一个 Singleton 后台任务需要动态读取订单自动取消时间，应该使用哪个接口？为什么？

**Q4.** `IOptionsMonitor<T>.CurrentValue` 是不是每次读取都会重新读取 JSON 文件？

**Q5.** 如果配置文件发生变化，但是 `IOptionsMonitor<T>` 没有更新，应该从哪些方面排查？

---

## 十、我的建议：这一阶段掌握到什么程度就够了？

考虑到我们的最终目标是 .NET 后端岗位面试，我建议 Options Pattern 不需要钻研到框架源码。

但以下知识必须能够独立解释：

- Configuration 和 Options 的区别。
- 强类型配置绑定。
- 三种 Options 的生命周期与缓存区别。
- 配置热更新原理。
- Singleton 与 Scoped 的依赖限制。
- Options 配置验证。

这些已经覆盖日常企业开发和大部分相关面试考察。

**下一步才进入真正的数据库开发：Step 05 — EF Core 10 + PostgreSQL。**

我们会从 DbContext、Entity、DbSet 开始，逐步完成数据库连接、Migration、Code First，最终实现真正的订单 CRUD。

先把本次实验做完。你可以把 `OptionsExperimentController` 和实际测试结果发给我，我继续帮你做 Code Review。

---

### 

## 三、IOptionsSnapshot<T>：每个 Scope 获取配置快照

这是三种 Options 中最容易被误解的一个。

### 1. 基本代码

将原来的 Controller 改成：

```csharp
[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly OrderSettings _settings;

    public SettingsController(
        IOptionsSnapshot<OrderSettings> options)
    {
        _settings = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            _settings.SystemName,
            _settings.MaxItemsPerOrder,
            _settings.EnableAutoCancel,
            _settings.AutoCancelMinutes
        });
    }
}
```

注意：这里只是将 `IOptions<OrderSettings>` 换成了 `IOptionsSnapshot<OrderSettings>`。

### 2. 它是如何工作的？

`IOptionsSnapshot<T>` 的特点：

- Scoped 生命周期。
- 每个 Scope 有自己的 Options 缓存。
- 同一个 Scope 内读取同一个配置名称，复用同一个配置对象。
- 新 Scope 可以根据当前配置重新创建配置对象。

在 ASP.NET Core Web API 中，通常一次 HTTP 请求对应一个 Scope。

我们来看一个例子。

初始配置：

```json
"MaxItemsPerOrder": 50
```

请求过程：

<box border radius="lg" padding={3} gap={2}>
  **HTTP Request 1**
  <box background="rgba(59,130,246,0.08)" padding={3} radius="md" gap={1}>
    <text weight="medium">Scope A</text>
    <row justify="between">
      <text color="secondary" size="sm">第一次读取</text>
      **50**
    </row>
    <row justify="between">
      <text color="secondary" size="sm">第二次读取</text>
      **50**
    </row>
    <caption>同一个 Scope 内使用缓存</caption>
  </box>
  <divider color="subtle"/>
  <text color="secondary" size="sm">修改 appsettings.json：50 → 100</text>
  <divider color="subtle"/>
  **HTTP Request 2**
  <box background="rgba(22,163,74,0.08)" padding={3} radius="md" gap={1}>
    <text weight="medium">Scope B</text>
    <row justify="between">
      <text color="secondary" size="sm">第一次读取</text>
      **100**
    </row>
    <row justify="between">
      <text color="secondary" size="sm">第二次读取</text>
      **100**
    </row>
    <caption>新 Scope 根据当前配置创建快照</caption>
  </box>
</box>

这里有一个重要细节：

**Snapshot 并不是每次访问 Value 都重新读取 JSON。**

它是在当前 Scope 第一次读取对应配置时创建并缓存对象。

所以同一个请求中，即使配置发生变化，已经创建的快照通常也不会改变。

### 3. 为什么不能注入 Singleton？

假设：

```csharp
public class OrderBackgroundService : BackgroundService
{
    private readonly IOptionsSnapshot<OrderSettings> _options;

    public OrderBackgroundService(
        IOptionsSnapshot<OrderSettings> options)
    {
        _options = options;
    }

    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
```

这段代码会出现 DI 生命周期问题。

原因：

- `BackgroundService` 是 Singleton。
- `IOptionsSnapshot<T>` 是 Scoped。
- Singleton 的生命周期比 Scoped 长。

在启用 Scope 验证时，会报类似这样的错误：

```text
Cannot consume scoped service
'IOptionsSnapshot<OrderSettings>'
from singleton
'OrderBackgroundService'.
```

这与之前学习的 Singleton 不能直接依赖 Scoped 是同一个问题。

### 4. 什么时候使用？

例如：

- 每个 HTTP 请求读取一份配置快照。
- 某些业务参数可能动态调整。
- 不要求同一个请求处理中实时切换配置。

企业 Web API 中，如果配置需要在后续请求中更新，`IOptionsSnapshot<T>` 是很自然的选择。

---

## 四、IOptionsMonitor<T>：动态配置监控

这是三种 Options 中功能最丰富的一种。

它特别适合 Singleton 服务和后台服务。

### 1. 基本代码

创建一个服务：

`Services/OrderMonitorService.cs`

```csharp
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Services;

public class OrderMonitorService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderMonitorService(
        IOptionsMonitor<OrderSettings> options)
    {
        _options = options;

        // 监听配置变化
        _options.OnChange(settings =>
        {
            Console.WriteLine(
                $"订单配置发生变化，最大商品数量：{settings.MaxItemsPerOrder}");
        });
    }

    public int GetMaxItems()
    {
        return _options.CurrentValue.MaxItemsPerOrder;
    }
}
```

在 `Program.cs` 注册：

```csharp
builder.Services.AddSingleton<OrderMonitorService>();
```

注意这里使用 Singleton。

因为 `IOptionsMonitor<T>` 本身就是 Singleton 生命周期，可以安全地注入 Singleton 服务。

### 2. CurrentValue 是什么？

```csharp
_options.CurrentValue.MaxItemsPerOrder
```

每次访问 `CurrentValue`，都会获取当前缓存的配置对象。

配置发生变化后，Options 系统会监听配置变更令牌，清除相应的缓存。

下一次访问 `CurrentValue` 时，会重新创建配置对象。

例如：

```text
应用启动
   |
   v
CurrentValue = 50
   |
   v
修改配置文件：50 → 100
   |
   v
配置系统触发变更通知
   |
   v
清除对应 Options 缓存
   |
   v
CurrentValue = 100
```

### 3. OnChange() 是什么？

```csharp
_options.OnChange(settings =>
{
    Console.WriteLine(
        $"配置已更新：{settings.MaxItemsPerOrder}");
});
```

它可以让程序主动感知配置变化。

例如：

- 修改订单最大商品数量。
- 修改某个业务开关。
- 修改第三方服务的连接参数。
- 动态调整某些后台任务参数。

需要注意，`OnChange()` 返回一个 `IDisposable`，用于取消监听。

如果长期运行的服务注册监听，建议保存这个对象，并在服务释放时正确 Dispose，避免重复订阅。

### 4. 为什么 Monitor 可以用于 Singleton？

因为：

```text
OrderMonitorService (Singleton)
           |
           v
IOptionsMonitor (Singleton)
           |
           v
OrderSettings 当前配置
```

它不会持有一个固定的 Scoped 配置快照。

而是通过内部缓存管理和配置变更通知机制，提供当前配置。

因此它非常适合后台任务：

```csharp
public class OrderBackgroundService : BackgroundService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderBackgroundService(
        IOptionsMonitor<OrderSettings> options)
    {
        _options = options;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var minutes = _options.CurrentValue.AutoCancelMinutes;

            Console.WriteLine($"当前自动取消时间：{minutes}");

            await Task.Delay(
                TimeSpan.FromMinutes(1),
                stoppingToken);
        }
    }
}
```

这里每次循环都会读取最新配置，而不是在构造函数中提前保存一个旧值。

不过要注意：如果修改的是正在执行中的任务参数，已经开始执行的那次业务操作是否切换到新参数，需要由业务代码明确决定。



---

### 

## 五、三种 Options 核心区别对比

现在把三个接口放在一起比较。

| 对比项 | IOptions | IOptionsSnapshot | IOptionsMonitor |
|---|---|---|---|
| DI 生命周期 | Singleton | Scoped | Singleton |
| 配置缓存 | 全局缓存 | 每个 Scope 缓存 | 全局缓存，可失效重建 |
| 同一请求内 | 不变 | 不变 | 可随更新变化 |
| 后续请求 | 使用原缓存 | 可获取新配置 | 可获取新配置 |
| 支持热更新 | 否 | 支持新 Scope 获取 | 支持 |
| OnChange | 不支持 | 不支持 | 支持 |
| 注入 Singleton | 可以 | 不可以 | 可以 |
| 典型用途 | 固定配置 | 请求级快照 | 动态配置、后台服务 |

特别强调两个区别：

**Snapshot 和 Monitor 都能获取更新后的配置，但机制不同。**

- Snapshot：新 Scope 获取新配置。
- Monitor：同一个服务实例也能获取更新后的配置。

这也是面试中很值得说明的地方。

## 六、配置热更新的前提条件

前面我们一直在讲修改 `appsettings.json`，但有一个前提：

配置源必须支持重新加载。

默认 ASP.NET Core WebApplication 的 JSON 配置提供程序通常启用了：

```csharp
reloadOnChange: true
```

可以显式理解为：

```csharp
builder.Configuration.AddJsonFile(
    "appsettings.json",
    optional: false,
    reloadOnChange: true);
```

但要注意，这段代码是额外添加配置提供程序的示例，不能直接不加判断地重复添加到默认配置链中。默认情况下已经注册了 JSON 配置文件。

如果配置来源是普通环境变量，修改操作通常需要重新启动进程，不能简单认为环境变量也会自动热更新。

另外，生产环境使用容器部署时，文件挂载、文件系统通知等也会影响配置变更能否被检测到。

## 七、企业项目中还有一个重要知识点：Options 验证

假设我们配置：

```json
"OrderSettings": {
  "SystemName": "",
  "MaxItemsPerOrder": -1,
  "EnableAutoCancel": true,
  "AutoCancelMinutes": 0
}
```

这些值明显不合理。

如果没有验证，程序可能正常启动，但运行到订单业务时才出现问题。

ASP.NET Core 支持配置验证。

修改 `OrderSettings.cs`：

```csharp
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Api.Settings;

public class OrderSettings
{
    [Required]
    public string SystemName { get; set; } = string.Empty;

    [Range(1, 100)]
    public int MaxItemsPerOrder { get; set; }

    public bool EnableAutoCancel { get; set; }

    [Range(1, 1440)]
    public int AutoCancelMinutes { get; set; }
}
```

修改 Program.cs：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

这段代码做了三件事：

1. `Bind()`：将配置绑定到 OrderSettings。
2. `ValidateDataAnnotations()`：使用 DataAnnotations 验证配置。
3. `ValidateOnStart()`：应用启动时验证配置。

如果配置不符合要求，应用启动就会失败，而不是等到业务运行时才暴露问题。

这是非常推荐的企业开发实践。

---

## 八、动手实验：在我们的项目中真正验证三种 Options

理论部分已经完整了。现在我们做一个实验，把三个接口放在同一个 API 中观察。

### 实验目标

我们准备测试：

- IOptions：是否始终读取旧配置。
- IOptionsSnapshot：新请求是否读取新配置。
- IOptionsMonitor：当前服务实例是否读取新配置。

### 第一步：创建 OptionsExperimentController

这次你自己写，要求如下：

构造函数注入：

```csharp
IOptions<OrderSettings>
IOptionsSnapshot<OrderSettings>
IOptionsMonitor<OrderSettings>
```

创建 GET 接口：

```http
GET /api/options-experiment
```

返回：

```json
{
  "options": 50,
  "snapshot": 50,
  "monitor": 50
}
```

提示：

```csharp
_options.Value.MaxItemsPerOrder

_snapshot.Value.MaxItemsPerOrder

_monitor.CurrentValue.MaxItemsPerOrder
```

### 第二步：运行实验

先将 JSON 配置设为：

```json
"MaxItemsPerOrder": 50
```

访问接口，确认三个值都是 50。

然后修改配置：

```json
"MaxItemsPerOrder": 100
```

保存文件，再次访问。

预期结果：

```json
{
  "options": 50,
  "snapshot": 100,
  "monitor": 100
}
```

这就是三种 Options 最直观的区别。

### 第三步：验证 Monitor 的 OnChange

将之前的 `OrderMonitorService` 注册为 Singleton，并通过构造函数注册 `OnChange()`。

修改 JSON 后观察 Rider 的运行控制台，是否出现：

```text
订单配置发生变化，最大商品数量：100
```

这样就把配置文件、配置绑定、DI、缓存、热更新全部串联起来了。

<box border radius="lg" padding={3} gap={2}>
  **本次实验验收**
  {@body const tasks = ["理解 IOptions 的全局缓存行为","理解 IOptionsSnapshot 的 Scope 缓存行为","理解 IOptionsMonitor 的 CurrentValue 和 OnChange","创建 OptionsExperimentController","修改配置文件并验证三种结果","完成 Options 配置验证"]}
  {@body const [done,setDone] = DIL.useState([])}
  <row align="center" justify="between">
    <text color="secondary" size="xs">完成进度</text>
    <badge color={done.length===tasks.length?"success":"secondary"}>{done.length}/{tasks.length}</badge>
  </row>
  <box background="surface-tertiary" radius="full" height="6px" clip>
    <box background="#16A34A" height="6px" radius="full" width={`${done.length/tasks.length*100}%`}/>
  </box>
  {#each tasks as task,i}
    <checkbox key={i} checked={done.includes(i)} onChange={v=>setDone(old=>v?[...old,i]:old.filter(x=>x!==i))}>{task}</checkbox>
  {/each}
</box>

## 九、面试模拟：检验是否真正掌握

尝试回答下面 5 道题：

**Q1.** `IOptions<T>` 和 `IOptionsSnapshot<T>` 最大区别是什么？

**Q2.** 为什么 `IOptionsSnapshot<T>` 不能注入 Singleton 服务？

**Q3.** 一个 Singleton 后台任务需要动态读取订单自动取消时间，应该使用哪个接口？为什么？

**Q4.** `IOptionsMonitor<T>.CurrentValue` 是不是每次读取都会重新读取 JSON 文件？

**Q5.** 如果配置文件发生变化，但是 `IOptionsMonitor<T>` 没有更新，应该从哪些方面排查？

---

## 十、我的建议：这一阶段掌握到什么程度就够了？

考虑到我们的最终目标是 .NET 后端岗位面试，我建议 Options Pattern 不需要钻研到框架源码。

但以下知识必须能够独立解释：

- Configuration 和 Options 的区别。
- 强类型配置绑定。
- 三种 Options 的生命周期与缓存区别。
- 配置热更新原理。
- Singleton 与 Scoped 的依赖限制。
- Options 配置验证。

这些已经覆盖日常企业开发和大部分相关面试考察。

**下一步才进入真正的数据库开发：Step 05 — EF Core 10 + PostgreSQL。**

我们会从 DbContext、Entity、DbSet 开始，逐步完成数据库连接、Migration、Code First，最终实现真正的订单 CRUD。

先把本次实验做完。你可以把 `OptionsExperimentController` 和实际测试结果发给我，我继续帮你做 Code Review。

---

### 

继续吧，深入理解三种 Options 这部分，内容好像没有输出完

---

### 

我们继续。这次我不重复前面三种 Options 的基础定义，而是再往实际开发深入一层。

前面已经讲过：

- `IOptions<T>`：全局缓存。
- `IOptionsSnapshot<T>`：Scope 级缓存。
- `IOptionsMonitor<T>`：支持配置变更通知。

接下来要解决几个更实际的问题：

1. 三种 Options 在 DI 容器内部究竟是怎样工作的？
2. `CurrentValue` 为什么不需要每次重新读取 JSON？
3. `IOptionsSnapshot` 为什么能在下一次请求拿到新配置？
4. `OnChange()` 是什么时候触发的？
5. 如果企业项目有几十个配置类，应该如何组织和验证？
6. 什么情况下应该使用 Options，什么情况下直接使用 IConfiguration？

这些理解之后，Options Pattern 才算真正掌握。

# 十一、深入理解 Options 内部工作机制

先看一个容易混淆的地方：

**Options 对象的创建，不等于配置文件的读取。**

例如：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

这段代码主要是在注册配置绑定规则，并不是每次执行它就创建一个 `OrderSettings` 实例。

当我们访问：

```csharp
options.Value
```

Options 系统才会根据注册的配置规则创建、绑定和缓存对应的对象。

其核心涉及几个内部组件：

| 内部组件 | 主要职责 |
|---|---|
| IOptionsFactory<T> | 创建并配置 Options 对象 |
| IOptionsMonitorCache<T> | 缓存 Monitor 使用的配置对象 |
| IOptionsMonitor<T> | 获取当前配置、监听变化 |
| IOptions<T> | 提供默认 Options 实例 |
| IOptionsSnapshot<T> | 提供 Scope 内的配置快照 |

例如：

```text
IOptionsFactory<OrderSettings>
          |
          v
创建 OrderSettings
          |
          v
执行 Configure / PostConfigure
          |
          v
执行验证器
          |
          v
返回最终配置对象
```

这里的 `Configure`、`PostConfigure` 和验证器都可以参与配置对象的创建过程。

这也解释了为什么 Options 不仅仅是简单的 JSON 反序列化。



---

### 

我们继续。这次我不重复前面三种 Options 的基础定义，而是再往实际开发深入一层。

前面已经讲过：

- `IOptions<T>`：全局缓存。
- `IOptionsSnapshot<T>`：Scope 级缓存。
- `IOptionsMonitor<T>`：支持配置变更通知。

接下来要解决几个更实际的问题：

1. 三种 Options 在 DI 容器内部究竟是怎样工作的？
2. `CurrentValue` 为什么不需要每次重新读取 JSON？
3. `IOptionsSnapshot` 为什么能在下一次请求拿到新配置？
4. `OnChange()` 是什么时候触发的？
5. 如果企业项目有几十个配置类，应该如何组织和验证？
6. 什么情况下应该使用 Options，什么情况下直接使用 IConfiguration？

这些理解之后，Options Pattern 才算真正掌握。

# 十一、深入理解 Options 内部工作机制

先看一个容易混淆的地方：

**Options 对象的创建，不等于配置文件的读取。**

例如：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

这段代码主要是在注册配置绑定规则，并不是每次执行它就创建一个 `OrderSettings` 实例。

当我们访问：

```csharp
options.Value
```

Options 系统才会根据注册的配置规则创建、绑定和缓存对应的对象。

其核心涉及几个内部组件：

| 内部组件 | 主要职责 |
|---|---|
| IOptionsFactory<T> | 创建并配置 Options 对象 |
| IOptionsMonitorCache<T> | 缓存 Monitor 使用的配置对象 |
| IOptionsMonitor<T> | 获取当前配置、监听变化 |
| IOptions<T> | 提供默认 Options 实例 |
| IOptionsSnapshot<T> | 提供 Scope 内的配置快照 |

例如：

```text
IOptionsFactory<OrderSettings>
          |
          v
创建 OrderSettings
          |
          v
执行 Configure / PostConfigure
          |
          v
执行验证器
          |
          v
返回最终配置对象
```

这里的 `Configure`、`PostConfigure` 和验证器都可以参与配置对象的创建过程。

这也解释了为什么 Options 不仅仅是简单的 JSON 反序列化。

## 十二、进一步理解：Named Options（命名配置）

这是前面没有展开的一个知识点。

假设我们的 OrderManagement 系统将来需要连接两个不同的支付服务：

- 支付宝配置
- 微信支付配置

如果只定义一个 `PaymentSettings`，如何区分两套配置？

这时可以使用 Named Options。

### 1. appsettings.json

```json
{
  "Payment": {
    "Alipay": {
      "AppId": "alipay-app-001",
      "TimeoutSeconds": 10
    },
    "WeChat": {
      "AppId": "wechat-app-001",
      "TimeoutSeconds": 30
    }
  }
}
```

### 2. 创建配置类

`Settings/PaymentSettings.cs`

```csharp
namespace OrderManagement.Api.Settings;

public class PaymentSettings
{
    public string AppId { get; set; } = string.Empty;

    public int TimeoutSeconds { get; set; }
}
```

### 3. Program.cs 注册两套命名配置

```csharp
builder.Services.Configure<PaymentSettings>(
    "Alipay",
    builder.Configuration.GetSection("Payment:Alipay"));

builder.Services.Configure<PaymentSettings>(
    "WeChat",
    builder.Configuration.GetSection("Payment:WeChat"));
```

这里的 `"Alipay"` 和 `"WeChat"` 就是配置实例的名称。

### 4. 在 Service 中读取

```csharp
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Services;

public class PaymentService
{
    private readonly IOptionsMonitor<PaymentSettings> _options;

    public PaymentService(
        IOptionsMonitor<PaymentSettings> options)
    {
        _options = options;
    }

    public PaymentSettings GetAlipaySettings()
    {
        return _options.Get("Alipay");
    }

    public PaymentSettings GetWeChatSettings()
    {
        return _options.Get("WeChat");
    }
}
```

注意这里不是：

```csharp
_options.CurrentValue
```

而是：

```csharp
_options.Get("Alipay");
_options.Get("WeChat");
```

因为 `CurrentValue` 代表默认名称的配置实例。

命名配置的读取规则：

| 接口 | 命名配置支持 |
|---|---|
| IOptions | 不支持按名称读取 |
| IOptionsSnapshot | 支持 `Get(name)` |
| IOptionsMonitor | 支持 `Get(name)` |

### 5. 这个功能在企业项目中有什么用？

例如：

```text
PaymentService
    |
    ├── AlipaySettings
    |
    ├── WeChatSettings
    |
    └── UnionPaySettings
```

也可以用于：

- 多个第三方 API。
- 多个邮件服务提供商。
- 多个外部系统连接参数。
- 同一服务的不同实例配置。

官方文档也将 Named Options 作为 Options Pattern 的一项重要能力。<Cite refs={["turn228102search1","turn228102search4"]}/>

---

## 十三、Configure、PostConfigure 和 Validate 的执行顺序

这部分更接近企业实际开发。

我们之前写过：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

其实还可以进一步定制配置对象。

例如：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .PostConfigure(options =>
    {
        if (string.IsNullOrWhiteSpace(options.SystemName))
        {
            options.SystemName = "DefaultOrderSystem";
        }
    })
    .Validate(
        options => options.MaxItemsPerOrder > 0,
        "MaxItemsPerOrder 必须大于 0")
    .ValidateOnStart();
```

我们分别解释。

**1. Configure / Bind**

负责读取原始配置：

```csharp
.Bind(builder.Configuration.GetSection("OrderSettings"))
```

**2. PostConfigure**

在所有普通配置操作之后，继续调整配置对象：

```csharp
.PostConfigure(options =>
{
    if (string.IsNullOrWhiteSpace(options.SystemName))
    {
        options.SystemName = "DefaultOrderSystem";
    }
})
```

**3. Validate**

验证最终配置是否合法：

```csharp
.Validate(
    options => options.MaxItemsPerOrder > 0,
    "MaxItemsPerOrder 必须大于 0")
```

**4. ValidateOnStart**

将验证提前到应用启动阶段。

完整执行顺序可以理解为：

<box border radius="lg" padding={3} gap={1} align="center">
  <box background="rgba(59,130,246,0.08)" radius="md" padding={2} width="100%" align="center">
    **Bind / Configure**
    <caption>读取并绑定配置</caption>
  </box>
  <icon name="arrow-down" color="secondary"/>
  <box background="rgba(59,130,246,0.08)" radius="md" padding={2} width="100%" align="center">
    **PostConfigure**
    <caption>后置调整配置</caption>
  </box>
  <icon name="arrow-down" color="secondary"/>
  <box background="rgba(22,163,74,0.08)" radius="md" padding={2} width="100%" align="center">
    **Validate**
    <caption>验证配置是否合法</caption>
  </box>
  <icon name="arrow-down" color="secondary"/>
  <box background="rgba(22,163,74,0.08)" radius="md" padding={2} width="100%" align="center">
    **Options Value**
    <caption>返回最终配置对象</caption>
  </box>
</box>

这里有一个值得记住的细节：

如果配置验证失败，`ValidateOnStart()` 可以让应用在启动时就发现问题，而不是等第一个业务请求访问配置时才报错。

如果没有使用 `ValidateOnStart()`，验证通常会在首次创建对应 Options 实例时执行；Monitor 热更新重新创建配置对象时，也会重新执行验证。<Cite refs={["turn228102search1","turn228102search3"]}/>

## 十四、一个容易被忽略的问题：Options 和 IConfiguration 怎么选？

这也是面试常见问题。

例如我们需要读取数据库连接字符串。

方式一：直接读取 IConfiguration。

```csharp
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");
```

方式二：使用 Options。

```csharp
public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}
```

注册：

```csharp
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));
```

那么应该选择哪种？

我的建议：

| 使用场景 | 推荐方式 |
|---|---|
| 读取一个简单配置值 | IConfiguration |
| 一组相关业务配置 | IOptions |
| 需要请求级配置快照 | IOptionsSnapshot |
| Singleton 中动态获取配置 | IOptionsMonitor |
| 需要配置验证 | Options Pattern |
| 配置项很多，需要分组管理 | Options Pattern |

实际项目中，两者并不冲突。

`IConfiguration` 是底层统一配置访问接口，Options 是建立在配置系统之上的强类型配置管理模式。

---

## 十五、特别注意：配置热更新不等于所有业务状态自动更新

这是一个很重要的生产环境问题。

假设我们有：

```csharp
public class OrderService
{
    private readonly OrderSettings _settings;

    public OrderService(IOptionsMonitor<OrderSettings> options)
    {
        _settings = options.CurrentValue;
    }

    public int GetMaxItems()
    {
        return _settings.MaxItemsPerOrder;
    }
}
```

虽然注入的是 `IOptionsMonitor`，但构造函数中：

```csharp
_settings = options.CurrentValue;
```

已经把当前配置对象保存下来了。

之后配置发生变化，`_settings` 不会自动替换。

正确的动态读取方式：

```csharp
public class OrderService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderService(IOptionsMonitor<OrderSettings> options)
    {
        _options = options;
    }

    public int GetMaxItems()
    {
        return _options.CurrentValue.MaxItemsPerOrder;
    }
}
```

这里每次调用都获取当前配置。

所以需要记住：

**使用 IOptionsMonitor，不代表你保存下来的旧配置对象会自动更新。要动态获取配置，就应该在需要时读取 CurrentValue。**

## 十六、最后做一个完整实验

现在我们把知识落实到项目中。

我建议暂时不再增加其他配置类，先围绕 `OrderSettings` 做一个实验。

### 实验 A：验证三种 Options

创建：

`Controllers/OptionsExperimentController.cs`

你自己完成，要求：

构造函数注入：

```csharp
IOptions<OrderSettings>
IOptionsSnapshot<OrderSettings>
IOptionsMonitor<OrderSettings>
```

GET：

```http
GET /api/options-experiment
```

返回：

```json
{
  "options": 50,
  "snapshot": 50,
  "monitor": 50
}
```

修改配置文件中的 `MaxItemsPerOrder`，从 50 改为 100。

再次请求，预期：

```json
{
  "options": 50,
  "snapshot": 100,
  "monitor": 100
}
```

前提：应用已经缓存过原配置，JSON Provider 的热加载正常，而且没有重启应用。

### 实验 B：验证 Monitor 的动态更新

保留一个 Singleton `OrderMonitorService`：

```csharp
builder.Services.AddSingleton<OrderMonitorService>();
```

在构造函数中注册 `OnChange()`。

修改 JSON，观察 Rider 控制台输出。

然后在 Controller 中调用：

```csharp
_monitorService.GetMaxItems()
```

观察即使 `OrderMonitorService` 没有重新创建，也能得到新值。

### 实验 C：验证配置合法性

把：

```json
"MaxItemsPerOrder": 100
```

改成：

```json
"MaxItemsPerOrder": -1
```

配置类：

```csharp
[Range(1, 100)]
public int MaxItemsPerOrder { get; set; }
```

Program.cs：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

观察应用启动时的行为。

注意：如果使用了文件热更新，非法配置也会触发重新绑定和验证；这时 Monitor 不会正常提供一个不合法的新配置对象。对于热更新配置，生产环境还应考虑如何处理更新失败，而不只是让程序在控制台报错。

---

## 十七、面试知识体系总结

<box border radius="lg" padding={3} gap={2}>
  **Options Pattern 知识清单**

  <badge color="success">必须掌握</badge>
  - IConfiguration 和 Options 的区别
  - Configuration Binding
  - IOptions / Snapshot / Monitor
  - 三种接口的生命周期
  - 缓存机制
  - 配置热更新
  - OnChange
  - ValidateOnStart
  - Singleton 与 Scoped 的依赖限制

  <divider color="subtle"/>
  <badge color="info">进阶了解</badge>
  - Named Options
  - Configure 和 PostConfigure
  - IOptionsFactory
  - IOptionsMonitorCache
  - 自定义 IValidateOptions
</box>

官方参考文档：[Options pattern in ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-10.0)

## 十八、检查一下你的理解

最后我给你三个场景题，看看是否真正掌握了。

{@body const questions=[{q:"一个普通 OrderService，需要读取固定的订单配置，运行期间不要求热更新，选择什么？",opts:["IConfiguration","IOptions<OrderSettings>","IOptionsSnapshot<OrderSettings>","IOptionsMonitor<OrderSettings>"],a:1,why:"一组固定的强类型业务配置，使用 IOptions 简洁合适。"},{q:"一个 Singleton BackgroundService，需要在运行期间动态获取最新订单自动取消时间，选择什么？",opts:["IOptions<OrderSettings>","IOptionsSnapshot<OrderSettings>","IOptionsMonitor<OrderSettings>","直接注入 Scoped"],a:2,why:"Monitor 是 Singleton，支持动态读取和配置变更通知。"},{q:"IOptionsMonitor 注入 Singleton 后，构造函数保存了 options.CurrentValue 到普通字段。配置更新后，这个字段会自动变化吗？",opts:["会，Monitor 会自动替换字段","不会，字段仍然引用原配置对象","每次请求自动重新创建 Singleton"],a:1,why:"Monitor 更新的是内部缓存。已经保存的旧对象引用不会自动替换。"}]}
{@body const [answers,setAnswers]=DIL.useState({})}
{@body const [submitted,setSubmitted]=DIL.useState(false)}
<box border radius="lg" padding={3} gap={3}>
  {#each questions as q,i}
    <box gap={2}>
      **问题 {i+1}：{q.q}**
      <radio-group direction="col" value={answers[i]===undefined?"":String(answers[i])} onChange={v=>{setAnswers(old=>({...old,[i]:Number(v)}));setSubmitted(false);}}>
        {#each q.opts as opt,j}
          <radio value={String(j)}>{opt}</radio>
        {/each}
      </radio-group>
      {#if submitted}
        <box background="surface-secondary" radius="md" padding={2} gap={1}>
          <row align="center" gap={2}>
            {#if answers[i]===q.a}
              <icon name="check-circle-filled" color="success"/>
              **回答正确**
            {:else}
              <icon name="x-circle" color="danger"/>
              **正确答案：{q.opts[q.a]}**
            {/if}
          </row>
          <text size="sm">{q.why}</text>
        </box>
      {/if}
    </box>
    {#if i!==2}
      <divider color="subtle"/>
    {/if}
  {/each}
  <button block disabled={Object.keys(answers).length!==3} onClick={()=>setSubmitted(true)}>提交答案</button>
</box>

**我的建议：** 到这里，Options Pattern 的学习就比较完整了。Named Options 和内部工厂机制知道原理即可，不必花大量时间研究源码。

下一步我们进入 Step 05：**EF Core 10 + PostgreSQL**。这会是整个项目真正开始产生业务价值的阶段：创建数据库、Entity、DbContext、Migration，然后实现订单持久化 CRUD。

---

### 

## 十二、进一步理解：Named Options（命名配置）

这是前面没有展开的一个知识点。

假设我们的 OrderManagement 系统将来需要连接两个不同的支付服务：

- 支付宝配置
- 微信支付配置

如果只定义一个 `PaymentSettings`，如何区分两套配置？

这时可以使用 Named Options。

### 1. appsettings.json

```json
{
  "Payment": {
    "Alipay": {
      "AppId": "alipay-app-001",
      "TimeoutSeconds": 10
    },
    "WeChat": {
      "AppId": "wechat-app-001",
      "TimeoutSeconds": 30
    }
  }
}
```

### 2. 创建配置类

`Settings/PaymentSettings.cs`

```csharp
namespace OrderManagement.Api.Settings;

public class PaymentSettings
{
    public string AppId { get; set; } = string.Empty;

    public int TimeoutSeconds { get; set; }
}
```

### 3. Program.cs 注册两套命名配置

```csharp
builder.Services.Configure<PaymentSettings>(
    "Alipay",
    builder.Configuration.GetSection("Payment:Alipay"));

builder.Services.Configure<PaymentSettings>(
    "WeChat",
    builder.Configuration.GetSection("Payment:WeChat"));
```

这里的 `"Alipay"` 和 `"WeChat"` 就是配置实例的名称。

### 4. 在 Service 中读取

```csharp
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Services;

public class PaymentService
{
    private readonly IOptionsMonitor<PaymentSettings> _options;

    public PaymentService(
        IOptionsMonitor<PaymentSettings> options)
    {
        _options = options;
    }

    public PaymentSettings GetAlipaySettings()
    {
        return _options.Get("Alipay");
    }

    public PaymentSettings GetWeChatSettings()
    {
        return _options.Get("WeChat");
    }
}
```

注意这里不是：

```csharp
_options.CurrentValue
```

而是：

```csharp
_options.Get("Alipay");
_options.Get("WeChat");
```

因为 `CurrentValue` 代表默认名称的配置实例。

命名配置的读取规则：

| 接口 | 命名配置支持 |
|---|---|
| IOptions | 不支持按名称读取 |
| IOptionsSnapshot | 支持 `Get(name)` |
| IOptionsMonitor | 支持 `Get(name)` |

### 5. 这个功能在企业项目中有什么用？

例如：

```text
PaymentService
    |
    ├── AlipaySettings
    |
    ├── WeChatSettings
    |
    └── UnionPaySettings
```

也可以用于：

- 多个第三方 API。
- 多个邮件服务提供商。
- 多个外部系统连接参数。
- 同一服务的不同实例配置。

官方文档也将 Named Options 作为 Options Pattern 的一项重要能力。<Cite refs={["turn228102search1","turn228102search4"]}/>

---

## 十三、Configure、PostConfigure 和 Validate 的执行顺序

这部分更接近企业实际开发。

我们之前写过：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

其实还可以进一步定制配置对象。

例如：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .PostConfigure(options =>
    {
        if (string.IsNullOrWhiteSpace(options.SystemName))
        {
            options.SystemName = "DefaultOrderSystem";
        }
    })
    .Validate(
        options => options.MaxItemsPerOrder > 0,
        "MaxItemsPerOrder 必须大于 0")
    .ValidateOnStart();
```

我们分别解释。

**1. Configure / Bind**

负责读取原始配置：

```csharp
.Bind(builder.Configuration.GetSection("OrderSettings"))
```

**2. PostConfigure**

在所有普通配置操作之后，继续调整配置对象：

```csharp
.PostConfigure(options =>
{
    if (string.IsNullOrWhiteSpace(options.SystemName))
    {
        options.SystemName = "DefaultOrderSystem";
    }
})
```

**3. Validate**

验证最终配置是否合法：

```csharp
.Validate(
    options => options.MaxItemsPerOrder > 0,
    "MaxItemsPerOrder 必须大于 0")
```

**4. ValidateOnStart**

将验证提前到应用启动阶段。

完整执行顺序可以理解为：

<box border radius="lg" padding={3} gap={1} align="center">
  <box background="rgba(59,130,246,0.08)" radius="md" padding={2} width="100%" align="center">
    **Bind / Configure**
    <caption>读取并绑定配置</caption>
  </box>
  <icon name="arrow-down" color="secondary"/>
  <box background="rgba(59,130,246,0.08)" radius="md" padding={2} width="100%" align="center">
    **PostConfigure**
    <caption>后置调整配置</caption>
  </box>
  <icon name="arrow-down" color="secondary"/>
  <box background="rgba(22,163,74,0.08)" radius="md" padding={2} width="100%" align="center">
    **Validate**
    <caption>验证配置是否合法</caption>
  </box>
  <icon name="arrow-down" color="secondary"/>
  <box background="rgba(22,163,74,0.08)" radius="md" padding={2} width="100%" align="center">
    **Options Value**
    <caption>返回最终配置对象</caption>
  </box>
</box>

这里有一个值得记住的细节：

如果配置验证失败，`ValidateOnStart()` 可以让应用在启动时就发现问题，而不是等第一个业务请求访问配置时才报错。

如果没有使用 `ValidateOnStart()`，验证通常会在首次创建对应 Options 实例时执行；Monitor 热更新重新创建配置对象时，也会重新执行验证。<Cite refs={["turn228102search1","turn228102search3"]}/>



---

### 

## 十四、一个容易被忽略的问题：Options 和 IConfiguration 怎么选？

这也是面试常见问题。

例如我们需要读取数据库连接字符串。

方式一：直接读取 IConfiguration。

```csharp
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");
```

方式二：使用 Options。

```csharp
public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}
```

注册：

```csharp
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));
```

那么应该选择哪种？

我的建议：

| 使用场景 | 推荐方式 |
|---|---|
| 读取一个简单配置值 | IConfiguration |
| 一组相关业务配置 | IOptions |
| 需要请求级配置快照 | IOptionsSnapshot |
| Singleton 中动态获取配置 | IOptionsMonitor |
| 需要配置验证 | Options Pattern |
| 配置项很多，需要分组管理 | Options Pattern |

实际项目中，两者并不冲突。

`IConfiguration` 是底层统一配置访问接口，Options 是建立在配置系统之上的强类型配置管理模式。

---

## 十五、特别注意：配置热更新不等于所有业务状态自动更新

这是一个很重要的生产环境问题。

假设我们有：

```csharp
public class OrderService
{
    private readonly OrderSettings _settings;

    public OrderService(IOptionsMonitor<OrderSettings> options)
    {
        _settings = options.CurrentValue;
    }

    public int GetMaxItems()
    {
        return _settings.MaxItemsPerOrder;
    }
}
```

虽然注入的是 `IOptionsMonitor`，但构造函数中：

```csharp
_settings = options.CurrentValue;
```

已经把当前配置对象保存下来了。

之后配置发生变化，`_settings` 不会自动替换。

正确的动态读取方式：

```csharp
public class OrderService
{
    private readonly IOptionsMonitor<OrderSettings> _options;

    public OrderService(IOptionsMonitor<OrderSettings> options)
    {
        _options = options;
    }

    public int GetMaxItems()
    {
        return _options.CurrentValue.MaxItemsPerOrder;
    }
}
```

这里每次调用都获取当前配置。

所以需要记住：

**使用 IOptionsMonitor，不代表你保存下来的旧配置对象会自动更新。要动态获取配置，就应该在需要时读取 CurrentValue。**

## 十六、最后做一个完整实验

现在我们把知识落实到项目中。

我建议暂时不再增加其他配置类，先围绕 `OrderSettings` 做一个实验。

### 实验 A：验证三种 Options

创建：

`Controllers/OptionsExperimentController.cs`

你自己完成，要求：

构造函数注入：

```csharp
IOptions<OrderSettings>
IOptionsSnapshot<OrderSettings>
IOptionsMonitor<OrderSettings>
```

GET：

```http
GET /api/options-experiment
```

返回：

```json
{
  "options": 50,
  "snapshot": 50,
  "monitor": 50
}
```

修改配置文件中的 `MaxItemsPerOrder`，从 50 改为 100。

再次请求，预期：

```json
{
  "options": 50,
  "snapshot": 100,
  "monitor": 100
}
```

前提：应用已经缓存过原配置，JSON Provider 的热加载正常，而且没有重启应用。

### 实验 B：验证 Monitor 的动态更新

保留一个 Singleton `OrderMonitorService`：

```csharp
builder.Services.AddSingleton<OrderMonitorService>();
```

在构造函数中注册 `OnChange()`。

修改 JSON，观察 Rider 控制台输出。

然后在 Controller 中调用：

```csharp
_monitorService.GetMaxItems()
```

观察即使 `OrderMonitorService` 没有重新创建，也能得到新值。

### 实验 C：验证配置合法性

把：

```json
"MaxItemsPerOrder": 100
```

改成：

```json
"MaxItemsPerOrder": -1
```

配置类：

```csharp
[Range(1, 100)]
public int MaxItemsPerOrder { get; set; }
```

Program.cs：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

观察应用启动时的行为。

注意：如果使用了文件热更新，非法配置也会触发重新绑定和验证；这时 Monitor 不会正常提供一个不合法的新配置对象。对于热更新配置，生产环境还应考虑如何处理更新失败，而不只是让程序在控制台报错。

---

## 十七、面试知识体系总结

<box border radius="lg" padding={3} gap={2}>
  **Options Pattern 知识清单**

  <badge color="success">必须掌握</badge>
  - IConfiguration 和 Options 的区别
  - Configuration Binding
  - IOptions / Snapshot / Monitor
  - 三种接口的生命周期
  - 缓存机制
  - 配置热更新
  - OnChange
  - ValidateOnStart
  - Singleton 与 Scoped 的依赖限制

  <divider color="subtle"/>
  <badge color="info">进阶了解</badge>
  - Named Options
  - Configure 和 PostConfigure
  - IOptionsFactory
  - IOptionsMonitorCache
  - 自定义 IValidateOptions
</box>

官方参考文档：[Options pattern in ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-10.0)

## 十八、检查一下你的理解

最后我给你三个场景题，看看是否真正掌握了。

{@body const questions=[{q:"一个普通 OrderService，需要读取固定的订单配置，运行期间不要求热更新，选择什么？",opts:["IConfiguration","IOptions<OrderSettings>","IOptionsSnapshot<OrderSettings>","IOptionsMonitor<OrderSettings>"],a:1,why:"一组固定的强类型业务配置，使用 IOptions 简洁合适。"},{q:"一个 Singleton BackgroundService，需要在运行期间动态获取最新订单自动取消时间，选择什么？",opts:["IOptions<OrderSettings>","IOptionsSnapshot<OrderSettings>","IOptionsMonitor<OrderSettings>","直接注入 Scoped"],a:2,why:"Monitor 是 Singleton，支持动态读取和配置变更通知。"},{q:"IOptionsMonitor 注入 Singleton 后，构造函数保存了 options.CurrentValue 到普通字段。配置更新后，这个字段会自动变化吗？",opts:["会，Monitor 会自动替换字段","不会，字段仍然引用原配置对象","每次请求自动重新创建 Singleton"],a:1,why:"Monitor 更新的是内部缓存。已经保存的旧对象引用不会自动替换。"}]}
{@body const [answers,setAnswers]=DIL.useState({})}
{@body const [submitted,setSubmitted]=DIL.useState(false)}
<box border radius="lg" padding={3} gap={3}>
  {#each questions as q,i}
    <box gap={2}>
      **问题 {i+1}：{q.q}**
      <radio-group direction="col" value={answers[i]===undefined?"":String(answers[i])} onChange={v=>{setAnswers(old=>({...old,[i]:Number(v)}));setSubmitted(false);}}>
        {#each q.opts as opt,j}
          <radio value={String(j)}>{opt}</radio>
        {/each}
      </radio-group>
      {#if submitted}
        <box background="surface-secondary" radius="md" padding={2} gap={1}>
          <row align="center" gap={2}>
            {#if answers[i]===q.a}
              <icon name="check-circle-filled" color="success"/>
              **回答正确**
            {:else}
              <icon name="x-circle" color="danger"/>
              **正确答案：{q.opts[q.a]}**
            {/if}
          </row>
          <text size="sm">{q.why}</text>
        </box>
      {/if}
    </box>
    {#if i!==2}
      <divider color="subtle"/>
    {/if}
  {/each}
  <button block disabled={Object.keys(answers).length!==3} onClick={()=>setSubmitted(true)}>提交答案</button>
</box>

**我的建议：** 到这里，Options Pattern 的学习就比较完整了。Named Options 和内部工厂机制知道原理即可，不必花大量时间研究源码。

下一步我们进入 Step 05：**EF Core 10 + PostgreSQL**。这会是整个项目真正开始产生业务价值的阶段：创建数据库、Entity、DbContext、Migration，然后实现订单持久化 CRUD。

---

### 

帮我总结提示词，我将切换到新的会话中，去继续执行任务。

---

### 

下面这份提示词可以直接复制到新会话中。它包含了项目背景、已完成进度、技术选择、教学方式，以及下一步任务，方便我们无缝继续。

<WritingBlock id="62841" variant="document"># ASP.NET Core 项目实战学习 — 上下文交接提示词

我正在通过一个 ASP.NET Core Web API 项目恢复 .NET 后端开发能力，同时准备 .NET 高级后端开发岗位面试。

请你继承以下项目背景和学习进度，继续一步一步指导我完成项目。**不要从头开始，也不要重复已经掌握的内容。**

## 一、学习目标与技术选型

- 开发环境：MacBook Air M3
- IDE：JetBrains Rider 为主，VS Code 为辅助
- .NET 版本：.NET 10
- 项目类型：ASP.NET Core Web API
- 数据库：PostgreSQL
- ORM：EF Core
- 教学方式：我自己动手写代码，你分步骤指导、检查和纠错。
- 学习目标：掌握现代 ASP.NET Core 企业级后端开发常用技术，能够应对实际项目开发和面试。

重点覆盖：
1. ASP.NET Core 基础架构与请求生命周期
2. DI 依赖注入及生命周期
3. Options Pattern 配置管理
4. EF Core + PostgreSQL
5. Entity、DbContext、Migration
6. CRUD、LINQ、异步查询
7. 数据验证与 DTO
8. 全局异常处理
9. 日志与结构化日志
10. 事务管理
11. 中间件与过滤器
12. Repository / Service 分层设计
13. API 设计规范
14. JWT 身份认证与授权
15. 缓存、性能优化及常见企业级实践

不要求一次性覆盖所有内容，应按照实际项目开发顺序逐步推进。

## 二、当前项目

项目名称：`OrderManagement`

Web API 项目：`OrderManagement.Api`

已经创建 Solution 和 Web API 项目，使用 .NET 10。

已经完成：
- Solution 和项目结构
- Program.cs 基础配置
- HelloController
- DI 生命周期实验
- Options Pattern 配置管理

## 三、已经完成的学习内容

### Step 02：项目基础

已经创建 ASP.NET Core Web API 项目，完成 Program.cs 和 HelloController。

### Step 03：DI 依赖注入

创建了：
- IOperationService
- OperationService
- ITransientOperationService
- IScopedOperationService
- ISingletonOperationService
- LifecycleController

Program.cs 注册：

```csharp
builder.Services.AddTransient<ITransientOperationService, OperationService>();
builder.Services.AddScoped<IScopedOperationService, OperationService>();
builder.Services.AddSingleton<ISingletonOperationService, OperationService>();
```

已经通过 Guid 实验理解：

- Transient：每次从 DI 容器解析时创建新实例。
- Scoped：同一个 Scope 内复用实例，通常一个 HTTP 请求一个 Scope。
- Singleton：整个应用生命周期复用同一个实例。

特别理解了：Transient 并不是每调用一次方法就创建一次实例，而是每次从容器解析时创建实例。

也了解了：
- 构造函数注入
- IServiceProvider.GetRequiredService
- DbContext 为什么通常注册为 Scoped
- Singleton 不应该直接依赖 Scoped 服务

### Step 04：Options Pattern

已经创建：

`Settings/OrderSettings.cs`

```csharp
namespace OrderManagement.Api.Settings;

public class OrderSettings
{
    public string SystemName { get; set; } = string.Empty;
    public int MaxItemsPerOrder { get; set; }
    public bool EnableAutoCancel { get; set; }
    public int AutoCancelMinutes { get; set; }
}
```

appsettings.json：

```json
{
  "OrderSettings": {
    "SystemName": "OrderManagement",
    "MaxItemsPerOrder": 50,
    "EnableAutoCancel": true,
    "AutoCancelMinutes": 30
  }
}
```

Program.cs：

```csharp
builder.Services.Configure<OrderSettings>(
    builder.Configuration.GetSection("OrderSettings"));
```

已经创建 SettingsController，通过 IOptions<OrderSettings> 获取配置。

已经深入学习：

**1. 三种 Options**

| 类型 | 生命周期 | 特点 |
|---|---|---|
| IOptions<T> | Singleton | 获取默认配置实例，不支持热更新 |
| IOptionsSnapshot<T> | Scoped | 每个 Scope 缓存配置实例，后续 Scope 可以读取更新后的配置 |
| IOptionsMonitor<T> | Singleton | 支持热更新、CurrentValue、Get(name)、OnChange |

**2. Options 内部机制**

已经理解：
- IOptionsFactory<T>：负责创建和配置 Options 实例。
- IOptionsMonitorCache<T>：负责 Monitor 实例缓存。
- Configure / Bind：绑定配置。
- PostConfigure：绑定后进一步修改配置。
- Validate：验证配置。
- ValidateOnStart：在应用启动时验证配置。

**3. Named Options**

了解了：

```csharp
builder.Services.Configure<PaymentSettings>(
    "Alipay",
    builder.Configuration.GetSection("Payment:Alipay"));

builder.Services.Configure<PaymentSettings>(
    "WeChat",
    builder.Configuration.GetSection("Payment:WeChat"));
```

可以通过：

```csharp
optionsSnapshot.Get("Alipay");
optionsMonitor.Get("WeChat");
```

获取指定名称的配置。

**4. 热更新机制**

已经理解：
- 配置源需要支持 reload，例如 JSON 配置文件的 reloadOnChange。
- IOptionsSnapshot 在同一个 Scope 内缓存，后续 Scope 才会看到更新。
- IOptionsMonitor 通过配置变更通知使缓存失效，下一次读取时重新创建配置实例。
- 如果在构造函数中将 CurrentValue 保存到普通字段，字段不会自动更新。
- 需要动态读取时，应在业务执行时访问 `_options.CurrentValue`。
- OnChange 返回 IDisposable，需要合理管理订阅生命周期。

**5. 配置验证**

已经学习：

```csharp
builder.Services
    .AddOptions<OrderSettings>()
    .Bind(builder.Configuration.GetSection("OrderSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

以及 `[Required]`、`[Range]` 和自定义 Validate。

**6. IConfiguration 与 Options**

已经理解：
- IConfiguration 适合读取单个配置项。
- Options 适合将一组相关配置封装成强类型对象，并支持验证、命名配置和热更新。

## 四、当前学习进度与下一步

目前 Options Pattern 的核心理论已经学习完成，但还需要通过实际代码验证三种 Options 的行为。

**下一步先不要直接进入 EF Core。**

请先带我完成一个综合实验：

### Step 04 实验：OptionsExperimentController

要求：
1. 同时注入：
   - IOptions<OrderSettings>
   - IOptionsSnapshot<OrderSettings>
   - IOptionsMonitor<OrderSettings>

2. 创建 GET 接口，例如：

`GET /api/options-experiment`

3. 返回三种 Options 当前的 `MaxItemsPerOrder`。

4. 修改 appsettings.json 中的配置：
   - 初始值设为 50。
   - 第一次请求后改为 100。
   - 不重启应用，再次请求。
   - 观察三种 Options 的返回结果。

预期理解：
- IOptions：仍然返回原来的 50。
- IOptionsSnapshot：新请求中读取到 100。
- IOptionsMonitor：读取到 100。

5. 后续再补充 OnChange 实验，验证配置变化通知。

请先给我这一小步的实现要求和必要代码提示，让我自己完成。不要一次性把整个后续项目代码全部给出。

## 五、教学要求

请严格遵循以下教学方式：

1. **循序渐进**：每次只推进一个明确的小步骤。
2. **先解释，再编码，再验证**：先讲清楚为什么需要这项技术，再给代码任务。
3. **让我自己动手**：优先给实现要求、代码骨架和关键提示，不要每次直接给完整答案。
4. **代码必须可运行**：涉及代码时，注意 .NET 10 的 API 和实际项目结构。
5. **代码审查**：我提交代码后，请逐段检查，指出错误、改进方式和背后的原因。
6. **关注面试**：每个重要知识点补充企业项目中的实际使用场景，以及常见面试问题。
7. **不要过度深入底层**：以企业级后端开发的常用知识和面试要求为主。
8. **不要重复已掌握内容**：如果之前已经讲过，就直接承接，不要从头讲一遍。
9. **保持主线**：这是一个完整的 OrderManagement 项目，不是零散的知识点演示。

## 六、后续项目推进建议

完成 Options 实验后，进入：

**Step 05：EF Core 10 + PostgreSQL**

建议顺序：
1. PostgreSQL 数据库准备及连接。
2. 安装 EF Core PostgreSQL Provider。
3. 创建 Order 实体。
4. 创建 ApplicationDbContext。
5. 配置连接字符串和 DI。
6. EF Core Migration。
7. 创建数据库和数据表。
8. 实现订单 CRUD。
9. 学习 LINQ 查询、异步查询、分页。
10. DTO 与 Entity 分离。
11. 数据验证。
12. Service 层与业务逻辑。
13. 事务处理。
14. 全局异常处理与日志。

后续再逐步完善为一个结构清晰、接近企业实际项目的 ASP.NET Core Web API。

---

**现在请从 Step 04 的 OptionsExperimentController 综合实验开始，不要重新讲解前面的基础内容。**</WritingBlock>

新会话中直接发送这份提示词即可。我们会从 **Options 三种配置方式的实际验证实验** 接着做，然后再进入 EF Core + PostgreSQL。

---

If you want, I can:

- 继续讲解 Options 相关实验步骤
- 提供代码示例实现 Options 实验
- 给出如何验证三种 Options 行为的详细实验步骤
