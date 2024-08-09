using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Business.Requests;
using Backend.Business.Mapping;

namespace Backend.Business.Services
{
    public class OrderService
    {
        private readonly SahibindenContext _context;

        public OrderService(SahibindenContext context)
        {
            _context = context;
        }

        public List<OrderRequestDTO> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.Menu)
                .Select(o => o.ToDto())
                .ToList();
        }

        public List<OrderRequestDTO> GetOrdersByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.Menu)
                .Where(o => o.UserId == userId)
                .Select(o => o.ToDto())
                .ToList();
        }


        public OrderRequestDTO? GetOrderById(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Menu)
                .FirstOrDefault(o => o.OrderId == orderId);
            return order?.ToDto(); // Nullable dönüş tipi belirtildi
        }


        public void AddOrder(OrderRequestDTO orderRequest)
        {
            try
            {
                var order = orderRequest.ToEntity();

                // Gerekirse burada UserId ve MenuId'nin geçerli olduğunu kontrol edebilirsiniz
                // Eğer bunlar geçerli değilse, bir hata fırlatabilirsiniz
                if (!_context.Users.Any(u => u.UserId == order.UserId))
                {
                    throw new Exception("Geçersiz UserId");
                }

                if (!_context.Menus.Any(m => m.MenuId == order.MenuId))
                {
                    throw new Exception("Geçersiz MenuId");
                }

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("DbUpdateException Hatası: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("İç Hata: " + ex.InnerException.Message);
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                throw;
            }
        }


        public void UpdateOrder(int orderId, OrderRequestDTO orderRequest)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.UserId = orderRequest.UserId;
                order.ProductType = orderRequest.ProductType;
                order.MenuId = orderRequest.MenuId;
                _context.SaveChanges();
            }
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
