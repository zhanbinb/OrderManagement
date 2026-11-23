// using Microsoft.AspNetCore.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
//
// namespace OrderManagement.Api.Exceptions;
//
// public class GlobalExceptionHandler(
//     ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
// {
//     public async ValueTask<bool> TryHandleAsync(
//         HttpContext httpContext,
//         Exception exception,
//         CancellationToken cancellationToken)
//     {
//         // 1. 记录异常日志
//         logger.LogError(
//             exception,
//             "Unhandled exception occurred. TraceId: {TraceId}",
//             httpContext.TraceIdentifier);
//
//         // 2. 构造统一错误响应
//         var problemDetails = new ProblemDetails
//         {
//             Status = StatusCodes.Status500InternalServerError,
//             Title = "服务器内部错误",
//             Detail = "请求处理过程中发生错误。"
//         };
//
//         // 3. 设置 HTTP 状态码
//         httpContext.Response.StatusCode =
//             StatusCodes.Status500InternalServerError;
//
//         // 4. 返回 JSON
//         await httpContext.Response.WriteAsJsonAsync(
//             problemDetails,
//             cancellationToken);
//
//         return true;
//     }
// }


using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            BusinessException => StatusCodes.Status409Conflict,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception");
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                400 => "Bad Request",
                404 => "Not Found",
                409 => "Business Conflict",
                _ => "Internal Server Error"
            },
            Detail = statusCode == 500
                ? "An unexpected error occurred."
                : exception.Message
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}