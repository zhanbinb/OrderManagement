using OrderManagement.Domain.Aggregates;

namespace OrderManagement.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(long orderId, CancellationToken cancellationToken=default);
    
    Task AddAsync(Order order, CancellationToken cancellationToken=default);
    
    Task SaveChangesAsync(CancellationToken cancellationToken=default);
}