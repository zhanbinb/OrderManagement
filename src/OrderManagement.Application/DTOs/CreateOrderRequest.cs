namespace OrderManagement.Application;

public class CreateOrderRequest
{
    public List<CreateOrderItemRequest> Items { get; set; } = [];
}