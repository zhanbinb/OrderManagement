using Microsoft.AspNetCore.Mvc.Filters;

namespace OrderManagement.Api.Filters;

public class ActionLogFilter : IAsyncActionFilter
{
    
    private readonly ILogger<ActionLogFilter> _logger;

    public ActionLogFilter(ILogger<ActionLogFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        _logger.LogInformation("开始执行 Action：{ActionName}", actionName);
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        _logger.LogInformation("Action Filter: Before Action");

        var result = await next();

        _logger.LogInformation("Action Filter: After Action");
        stopwatch.Stop();

        _logger.LogInformation(
            "Action completed: {ActionName}, Elapsed: {Elapsed}ms",
            actionName,
            stopwatch.ElapsedMilliseconds);

    }
}