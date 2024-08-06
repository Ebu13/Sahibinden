using Microsoft.AspNetCore.Mvc;
using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
        public ActionResult<List<OrderRequestDTO>> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Buyer")]
        public ActionResult<List<OrderRequestDTO>> GetOrdersByUserId(int userId)
        {
            var orders = _orderService.GetOrdersByUserId(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return orders;
        }


        [HttpGet("{id}")]
        [Authorize]
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
        [Authorize(Roles = "Buyer")]
        public ActionResult<OrderRequestDTO> AddOrder(OrderRequestDTO orderRequest)
        {
            try
            {
                _orderService.AddOrder(orderRequest);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderRequest.OrderId }, orderRequest);
            }
            catch (Exception ex)
            {
                // Hata günlüğü yazma
                return BadRequest(new { message = "Sipariş oluşturulurken bir hata oluştu.", error = ex.Message });
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Buyer")]
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
        [Authorize(Roles = "Buyer")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
