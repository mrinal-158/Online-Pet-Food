using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Pet_Food.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("Create-Order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            var userId = int.Parse(userIdClaim.Value);

            var order = await _orderService.CreateOrderAsync(userId, createOrderDto);

            return Ok(order);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("My-Orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            var userId = int.Parse(userIdClaim.Value);

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("Cancel-Order/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            var userId = int.Parse(userIdClaim.Value);

            var result = await _orderService.CancelOrderAsync(userId, orderId);

            if (result == "Unauthorize") return BadRequest(new { Message = "Unable to cancel order" });
            if (result == "Paid") return BadRequest(new { Message = "Unable to cancel order because it has been paid" });
            return Ok(new { Message = "Order cancelled successfully" });
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("Pay-Order/{orderId}")]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            var userId = int.Parse(userIdClaim.Value);

            var result = await _orderService.PayOrderAsync(userId, orderId);
            if(result != "Success") return BadRequest(new { Message = result });

            return Ok(new { Message = "Order Confirm!" });
        }
    }
}
