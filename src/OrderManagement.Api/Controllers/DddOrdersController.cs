using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controllers
{
    [Route("api/ddd/orders")]
    [ApiController]
    public class DddOrdersController : ControllerBase
    {
        private readonly OrderApplicationService _orderService;

        public DddOrdersController(OrderApplicationService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var orderId = await _orderService.CreateOrderAsync(
                request, cancellationToken);

            return Created(
                $"/api/ddd/orders/{orderId}",
                new { OrderId = orderId });
        }
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(
            long id,
            CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderDetailAsync(
                id, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPut("{orderId:long}/items/{itemId:guid}/quantity")]
        public async Task<IActionResult> UpdateItemQuantity(
            long orderId,
            Guid itemId,
            [FromBody] UpdateOrderItemQuantityRequest request,
            CancellationToken cancellationToken)
        {
            var updated = await _orderService.UpdateOrderItemQuantityAsync(
                orderId,
                itemId,
                request.Quantity,
                cancellationToken);

            if (!updated)
                return NotFound("Order not found");

            return NoContent();
        }
        
        [HttpPost("{orderId:long}/cancel")]
        public async Task<IActionResult> Cancel(
            long orderId,
            CancellationToken cancellationToken)
        {
            var cancelled = await _orderService.CancelOrderAsync(
                orderId, cancellationToken);

            if (!cancelled)
                return NotFound("Order not found");

            return NoContent();
        }
    }
}