namespace OrderManagement.Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new ArgumentException("Currencies must match");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Multiply(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantity must be non-negative");
        
        return new Money(Amount * quantity, Currency);
    }
}