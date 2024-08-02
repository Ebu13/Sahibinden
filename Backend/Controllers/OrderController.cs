using Microsoft.AspNetCore.Mvc;
using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<OrderRequestDTO>> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [HttpGet("{id}")]
        public ActionResult<OrderRequestDTO> GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public ActionResult<OrderRequestDTO> AddOrder(OrderRequestDTO orderRequest)
        {
            _orderService.AddOrder(orderRequest);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderRequest.OrderId }, orderRequest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, OrderRequestDTO orderRequest)
        {
            if (id != orderRequest.OrderId)
            {
                return BadRequest();
            }

            _orderService.UpdateOrder(id, orderRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
