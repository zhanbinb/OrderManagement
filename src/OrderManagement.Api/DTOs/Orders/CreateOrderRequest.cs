using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Api.DTOs.Orders;

public class CreateOrderRequest
{
    [Required(ErrorMessage="订单编号不能为空")]
    [StringLength(50, ErrorMessage="订单编号长度不能超过50个字符")]
    public string OrderNo { get; set; } = string.Empty;

    [Required(ErrorMessage="客户名称不能为空")]
    [StringLength(100, ErrorMessage="客户名称长度不能超过100个字符")]
    public string CustomerName { get; set; } = string.Empty;

    [Range(0.01,99999999, ErrorMessage="总金额必须大于0")]
    public decimal TotalAmount { get; set; }
    
}