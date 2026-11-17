using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using OrderManagement.Api.DTOs.Orders;
using OrderManagement.Api.Entities;
using OrderManagement.Api.Enums;

namespace OrderManagement.Api.Services.Orders;

public class OrderService : IOrderService
{
    private const string NullCacheValue = "__NULL__";

    private static readonly TimeSpan OrderCacheDuration = TimeSpan.FromMinutes(2);
    private static readonly TimeSpan NullCacheDuration = TimeSpan.FromSeconds(60);

    private readonly IOrderRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository repository,
        IDistributedCache cache,
        ILogger<OrderService> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<Order> CreateAsync(CreateOrderRequest request)
    {
        _logger.LogInformation("开始创建订单，订单编号：{OrderNo}", request.OrderNo);
        var order = new Order
        {
            CustomerName = request.CustomerName,
            OrderNo = request.OrderNo,
            TotalAmount = request.TotalAmount,
            Status = OrderStatus.Pending,
        };
        await _repository.AddAsync(order);
        _logger.LogInformation("订单创建成功，订单编号：{OrderNo}", order.OrderNo);
        return order;
    }

    public async Task<OrderDetailDto?> GetByIdAsync(int id)
    {
        var cacheKey = GetCacheKey(id);

        // 1. 先查询缓存
        var cachedOrder = await _cache.GetStringAsync(cacheKey);
        if (cachedOrder is not null)
        {
            // 命中空值缓存，直接返回不存在
            if (cachedOrder == NullCacheValue)
            {
                _logger.LogInformation("订单空值缓存命中，OrderId: {OrderId}", id);
                return null;
            }

            var orderFromCache = JsonSerializer.Deserialize<OrderDetailDto>(cachedOrder);
            if (orderFromCache is not null)
            {
                _logger.LogInformation(
                    "订单详情查询成功（缓存命中）。OrderId: {OrderId}", id);
                return orderFromCache;
            }
        }

        // 2. 缓存未命中，查询数据库
        _logger.LogInformation("订单缓存未命中，查询数据库。OrderId: {OrderId}", id);
        var order = await _repository.GetByIdAsync(id);
        if (order is null)
        {
            await _cache.SetStringAsync(cacheKey, NullCacheValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = NullCacheDuration
            });

            _logger.LogInformation("订单不存在，已写入空值缓存。OrderId: {OrderId}", id);
            return null;
        }

        // 3. 将数据库结果写入缓存
        var detail = MapToDetailDto(order);
        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(detail),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = OrderCacheDuration
            });

        return detail;
    }

    public async Task<OrderListResponse> GetListAsync(int pageIndex, int pageSize)
    {
        var totalCount = await _repository.CountAsync();
        var orders = await _repository.GetPagedAsync(pageIndex, pageSize);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new OrderListResponse
        {
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalPages = totalPages,
            Items = orders.Select(MapToListItemDto).ToList()
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateOrderRequest request)
    {
        var order = await _repository.GetByIdAsync(id, asNoTracking: false);
        if (order is null)
        {
            return false;
        }
        _logger.LogInformation(
            "订单并发检查：OrderId={OrderId}, DatabaseVersion={DatabaseVersion}, RequestVersion={RequestVersion}",
            id,
            order.Version,
            request.Version);
        // 检查客户端提交的版本
        if (order.Version != request.Version)
        {
            throw new DbUpdateConcurrencyException(
                "订单已被其他用户修改，请刷新后重试。");
        }
        
        order.CustomerName = request.CustomerName;
        order.TotalAmount = request.TotalAmount;
        order.UpdatedAt = DateTime.UtcNow;
        try
        {
            await _repository.UpdateAsync(order);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new DbUpdateConcurrencyException(
                "订单已被其他用户修改，请刷新后重试。");
        }

        // 数据库更新成功后，删除 Redis 缓存
        await _cache.RemoveAsync(GetCacheKey(id));
        _logger.LogInformation("订单更新成功，缓存已删除。OrderId: {OrderId}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id, asNoTracking: false);
        if (order is null)
        {
            return false;
        }

        await _repository.DeleteAsync(order);

        // 数据库删除成功后，删除 Redis 缓存
        await _cache.RemoveAsync(GetCacheKey(id));
        _logger.LogInformation("订单删除成功，缓存已删除。OrderId: {OrderId}", id);
        return true;
    }

    public async Task<List<OrderListItemDto>> GetPendingAsync()
    {
        var orders = await _repository.GetByStatusAsync(OrderStatus.Pending);
        _logger.LogInformation("查询待处理订单，共 {Count} 条", orders.Count);
        return orders.Select(MapToListItemDto).ToList();
    }

    private static string GetCacheKey(int id) => $"order:{id}";

    private static OrderDetailDto MapToDetailDto(Order order)
    {
        return new OrderDetailDto
        {
            Id = order.Id,
            OrderNo = order.OrderNo,
            CustomerName = order.CustomerName,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Version = order.Version
        };
    }

    private static OrderListItemDto MapToListItemDto(Order order)
    {
        return new OrderListItemDto
        {
            Id = order.Id,
            OrderNo = order.OrderNo,
            CustomerName = order.CustomerName,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt
        };
    }
}
