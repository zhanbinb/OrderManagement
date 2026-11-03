using System;
using System.Collections.Generic;
using OrderManagement.Api.Enums;

namespace OrderManagement.Api.Entities;

public class Order
{
    public int Id { get; set; }

    public string OrderNo { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
    

}