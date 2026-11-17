using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Services.Orders;

namespace OrderManagement.Api.Controllers;

[EnableRateLimiting("order-api")]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;

    public OrdersController(
        ILogger<OrdersController> logger,
        IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var order = await _orderService.CreateAsync(request);

        return StatusCode(StatusCodes.Status201Created, order);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(int pageIndex = 1, int pageSize = 10)
    {
        // 1. 校验分页参数
        // pageIndex 最小为 1
        if (pageIndex < 1)
        {
            return BadRequest("pageIndex must be greater than or equal to 1");
        }
        // pageSize 最小为 1，最大为 100
        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest("pageSize must be between 1 and 100");
        }

        // 2. 查询并组装分页结果
        var response = await _orderService.GetListAsync(pageIndex, pageSize);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateOrderRequest request)
    {
        try
        {
            var updated = await _orderService.UpdateAsync(id, request);
        
            if (!updated)
            {
                return NotFound();
            }
        
            return NoContent();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
       
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _orderService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        _logger.LogInformation("测试异常日志");
        throw new Exception("测试异常");
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingOrders()
    {
        var orders = await _orderService.GetPendingAsync();

        return Ok(orders);
    }

    [HttpGet("secure")]
    [Authorize]
    public IActionResult GetSecure()
    {
        return Ok(new
        {
            Message = "This is a secure endpoint",
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            Username = User.Identity?.Name,
            IsAuthenticated = User.Identity?.IsAuthenticated,
        });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminOrders()
    {
        return Ok(new
        {
            Message = "This is an admin endpoint",
            Username = User.Identity?.Name,
            Role = User.FindFirst(ClaimTypes.Role)?.Value,
        });
    }
}
