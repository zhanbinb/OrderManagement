using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsyncDemoController : ControllerBase
{
    // GET
    [HttpGet("sync")]
    public IActionResult Sync()
    {
        Thread.Sleep(3000); // 模拟耗时操作
        return Ok(new
        {
            Message = "同步请求",
            ThreadId = Environment.CurrentManagedThreadId
        });
    }
    
    [HttpGet("async")]
    public async Task<IActionResult> Async()
    {
        var startThreadId = Environment.CurrentManagedThreadId;
        await Task.Delay(3000); // 模拟耗时操作
        return Ok(new
        {
            Message = "Async completed",
            StartThreadId = startThreadId,
            EndThreadId = Environment.CurrentManagedThreadId
        });
    }
    
    [HttpGet("async-parallel")]
    public async Task<IActionResult> AsyncParallel()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var task1 = Task.Delay(3000);
        var task2 = Task.Delay(3000);

        await Task.WhenAll(task1, task2);

        stopwatch.Stop();

        return Ok(new
        {
            Message = "Parallel completed",
            ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
        });
    }
    
    [HttpGet("when-all")]
    public async Task<IActionResult> WhenAllDemo()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var task1 = Task.Delay(2000);
        var task2 = Task.Delay(3000);
        var task3 = Task.Delay(4000);

        await Task.WhenAll(task1, task2, task3);

        stopwatch.Stop();

        return Ok(new
        {
            Message = "WhenAll completed",
            ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
        });
    }
    
    [HttpGet("thread")]
    public IActionResult ThreadDemo()
    {
        var thread = new Thread(() =>
        {
            Console.WriteLine(
                $"Thread ID: {Environment.CurrentManagedThreadId}");
        });

        thread.Start();
        thread.Join();

        return Ok("Thread completed");
    }

    [HttpGet("task-run")]
    public async Task<IActionResult> TaskRunDemo()
    {
        var threadId = await Task.Run(() =>
        {
            return Environment.CurrentManagedThreadId;
        });

        return Ok(new
        {
            Message = "Task.Run completed",
            ThreadId = threadId
        });
    }
}
