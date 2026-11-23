using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public long ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public Money UnitPrice { get; private set; }= new Money(0, "CNY");
    public int Quantity { get; private set; }
    
    public Money Subtotal=> UnitPrice.Multiply(quantity: Quantity);

    private OrderItem()
    {
    }
    internal OrderItem(long productId, string productName, Money unitPrice, int quantity)
    {

        if (productId < 0)
            throw new ArgumentException("Invalid product ID");
        if(string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be empty");
        if (unitPrice.Amount <= 0)
            throw new ArgumentException("Unit price must be greater than zero");
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
    
    internal void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
        Quantity = newQuantity;
    }

}