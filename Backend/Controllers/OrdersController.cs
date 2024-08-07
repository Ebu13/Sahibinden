using Backend.Business.Requests;
using Backend.Business.Services;
using Backend.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILoggerService _logger;

        public OrdersController(OrderService orderService, ILoggerService logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<OrderRequestDTO>> GetAllOrders()
        {
            _logger.LogInfo("GetAllOrders endpoint hit.");
            try
            {
                var orders = _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllOrders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Buyer")]
        public ActionResult<List<OrderRequestDTO>> GetOrdersByUserId(int userId)
        {
            _logger.LogInfo($"GetOrdersByUserId endpoint hit with userId: {userId}");
            try
            {
                var orders = _orderService.GetOrdersByUserId(userId);
                if (orders == null || !orders.Any())
                {
                    _logger.LogWarn($"No orders found for userId: {userId}");
                    return NotFound();
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrdersByUserId: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<OrderRequestDTO> GetOrderById(int id)
        {
            _logger.LogInfo($"GetOrderById endpoint hit with id: {id}");
            try
            {
                var order = _orderService.GetOrderById(id);
                if (order == null)
                {
                    _logger.LogWarn($"Order with id: {id} not found.");
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrderById: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public ActionResult<OrderRequestDTO> AddOrder(OrderRequestDTO orderRequest)
        {
            _logger.LogInfo("AddOrder endpoint hit.");
            try
            {
                _orderService.AddOrder(orderRequest);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderRequest.OrderId }, orderRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddOrder: {ex.Message}");
                return BadRequest(new { message = "Sipariş oluşturulurken bir hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult UpdateOrder(int id, OrderRequestDTO orderRequest)
        {
            _logger.LogInfo($"UpdateOrder endpoint hit with id: {id}");
            if (id != orderRequest.OrderId)
            {
                _logger.LogWarn($"UpdateOrder failed due to id mismatch: {id} != {orderRequest.OrderId}");
                return BadRequest();
            }

            try
            {
                _orderService.UpdateOrder(id, orderRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateOrder: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult DeleteOrder(int id)
        {
            _logger.LogInfo($"DeleteOrder endpoint hit with id: {id}");
            try
            {
                _orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteOrder: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
