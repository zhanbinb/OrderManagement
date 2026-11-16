using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using OrderManagement.Api.Data;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;
using OrderManagement.Api.Services.Orders;

namespace OrderManagement.Api.Controllers;

[EnableRateLimiting("order-api")]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    private readonly ILogger<OrdersController> _logger;
    
    private readonly IOrderService _orderService;

    private readonly IMemoryCache _cache;
    
    private readonly IDistributedCache _distributedCache;

    public OrdersController(ApplicationDbContext context,ILogger<OrdersController> logger, IOrderService orderService,IMemoryCache cache,IDistributedCache distributedCache)
    {
        _context = context;
        _logger = logger;
        _orderService = orderService;
        _cache = cache;
        _distributedCache = distributedCache;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateOrderRequest request)
    {
        // // 1. 将 Request DTO 转换为 Order Entity
        // var order = new Order
        // {
        //     OrderNo = request.OrderNo,
        //     CustomerName = request.CustomerName,
        //     TotalAmount = request.TotalAmount,
        // };
        //
        //
        // // 2. 将 Entity 添加到 DbContext
        // _context.Orders.Add(order);
        // // 3. 保存到数据库
        // await _context.SaveChangesAsync();

        var order = await _orderService.CreateAsync(request);
        
        

        // 4. 返回 201 Created
        return StatusCode(201, order);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        // var cacheKey = $"order:{id}";
        //
        // var order =await _cache.GetOrCreateAsync(cacheKey, async entry =>
        // {
        //     entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
        //     _logger.LogInformation("订单缓存未命中，查询数据库。OrderId: {OrderId}",
        //         id);
        //     return await _context.Orders.FindAsync(id);
        // });
        //
        // if (order == null)
        // {
        //     return NotFound();
        // }
        // _logger.LogInformation(
        //     "订单详情查询成功。OrderId: {OrderId}",
        //     id);
        
        var cacheKey = $"order:{id}";
        // 1.先查询redis
        var cachedOrder = await _distributedCache.GetStringAsync(cacheKey);
        if (cachedOrder is not null)
        {
            //如果命中空值缓存，直接返回 40477
            if (cachedOrder == "__NULL__")
            {
                _logger.LogInformation(
                    "订单空值缓存命中，OrderId: {OrderId}", id);

                return NotFound();
            }
            var orderFromCache=JsonSerializer.Deserialize<Order>(cachedOrder);
            if(orderFromCache is not null)
            {
                _logger.LogInformation(
                    "订单详情查询成功（缓存命中）。OrderId: {OrderId}",
                    id);
                return Ok(orderFromCache);
            }
        }
        
        //2.缓存未命中，查询数据库
        _logger.LogInformation("订单缓存未命中，查询数据库。OrderId: {OrderId}",id);
            var order =await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
            if (order is null)
            {
                await _distributedCache.SetStringAsync(cacheKey,"__NULL__",new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                });
                
                _logger.LogInformation(
                    "订单不存在，已写入空值缓存。OrderId: {OrderId}", id);
                return NotFound();
            }
            // 3. 将数据库结果写入 Redis
            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(order),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                });

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
        
        //数据库更新成功后，删除Redis 缓存
        var cacheKey = $"order:{id}";
        await _distributedCache.RemoveAsync(cacheKey);
        _logger.LogInformation("订单缓存已删除。OrderId: {OrderId}", id);
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
        
        //删除Redis缓存
        var cacheKey = $"order:{id}";
        await _distributedCache.RemoveAsync(cacheKey);
        _logger.LogInformation("订单缓存已删除。OrderId: {OrderId}", id);
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
        var query = _context.Orders.AsNoTracking()
            .Where(x => x.Status == OrderStatus.Pending)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new OrderListItemDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                CustomerName = x.CustomerName,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            });
        _logger.LogInformation(
            "Pending Orders SQL: {Sql}",
            query.ToQueryString());

        var orders = await query.ToListAsync();

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