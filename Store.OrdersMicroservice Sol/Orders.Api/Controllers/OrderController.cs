using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.BusinessLogic.Dtos;
using Orders.BusinessLogic.ServicesContracts;

namespace Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> MakeOrder(AddOrderDto input)
        {
            if (input == null) return BadRequest("Order Input Can't Be Empty.");
            var order = await _orderService.CreateOrder(input);
            if (order == null)
                return BadRequest("Order could not be created.");

            return CreatedAtAction(nameof(Get), new { orderId = order.Id }, order);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetOrders());
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get (Guid orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null) return NotFound("Order Does Not Exist.");
            return Ok(order);
        }
        [HttpGet("search/{userEmail}")]
        public async Task<IActionResult> Get (string userEmail)
        {
            var orders = await _orderService.GetOrderByBuyerEmail(userEmail);
            if (orders == null) return NotFound("No Orders Found.");
            return Ok(orders);
        }
    }
}
