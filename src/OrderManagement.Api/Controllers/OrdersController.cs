using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Data;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ApplicationDbContext context,ILogger<OrdersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateOrderRequest request)
    {
        // 1. 将 Request DTO 转换为 Order Entity
        var order = new Order
        {
            OrderNo = request.OrderNo,
            CustomerName = request.CustomerName,
            TotalAmount = request.TotalAmount,
        };


        // 2. 将 Entity 添加到 DbContext
        _context.Orders.Add(order);
        // 3. 保存到数据库
        await _context.SaveChangesAsync();

        // 4. 返回 201 Created
        return StatusCode(201, order);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
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
        if (pageIndex<1)
        {
            return BadRequest("pageIndex must be greater than or equal to 1");
        }
        // pageSize 最小为 1，最大为 100
        if (pageSize<1 || pageSize>100)
        {
            return BadRequest("pageSize must be between 1 and 100");
        }
        // 2. 查询总记录数
        var totalCount = await _context.Orders.CountAsync();
        // 3. 查询当前页订单
        var orders = await _context.Orders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new OrderListItemDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                CustomerName = x.CustomerName,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
        // 4. 计算总页数
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        // 5. 组装 OrderListResponse
        var response = new OrderListResponse
        {
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalPages = totalPages,
            Items = orders
        };
        // 6. 返回 Ok(response)

        return Ok(response);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateOrderRequest request)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        order.CustomerName = request.CustomerName;
        order.TotalAmount = request.TotalAmount;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        _logger.LogInformation("测试异常日志");
        throw new Exception("测试异常");
    }
}