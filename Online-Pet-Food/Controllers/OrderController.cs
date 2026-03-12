using Application.DTOs;
using Application.Interfaces;
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

        [HttpPost("Create-Order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            //var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            //var userId = int.Parse(userIdClaim.Value);
            var userId = 1; // Replace with actual user ID retrieval logic

            var order = await _orderService.CreateOrderAsync(userId, createOrderDto);

            return Ok(order);
        }
    }
}
