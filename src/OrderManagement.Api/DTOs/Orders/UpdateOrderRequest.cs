using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Api.DTOs.Orders;

public class UpdateOrderRequest
{
    [Required(ErrorMessage = "客户名称不能为空")]
    [StringLength(100, ErrorMessage = "客户名称不能超过100个字符")]
    public string CustomerName { get; set; } = string.Empty;

    [Range(0.01, 999999999, ErrorMessage = "订单金额必须大于0")]
    public decimal TotalAmount { get; set; }

    [StringLength(500, ErrorMessage = "备注不能超过500个字符")]
    public string? Remark { get; set; }
}