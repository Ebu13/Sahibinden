using Backend.Business.Requests;
using Backend.Models;

namespace Backend.Business.Mapping
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps CarRequestDto to Car entity.
        /// </summary>
        /// <param name="carRequest">The CarRequestDto object.</param>
        /// <returns>The Car entity.</returns>
        public static Car ToEntity(this CarRequestDto carRequest)
        {
            return new Car
            {
                CarId = carRequest.CarId,
                UserId = carRequest.UserId,
                MenuId = carRequest.MenuId,
                Year = carRequest.Year,
                Price = carRequest.Price,
                PhotoPath = carRequest.PhotoPath
            };
        }

        /// <summary>
        /// Maps Car entity to CarRequestDto.
        /// </summary>
        /// <param name="car">The Car entity.</param>
        /// <returns>The CarRequestDto object.</returns>
        public static CarRequestDto ToDto(this Car car)
        {
            return new CarRequestDto
            {
                CarId = car.CarId,
                UserId = car.UserId,
                MenuId = car.MenuId,
                Year = car.Year,
                Price = car.Price,
                PhotoPath = car.PhotoPath
            };
        }

        /// <summary>
        /// Maps HomeRequestDto to Home entity.
        /// </summary>
        /// <param name="homeRequest">The HomeRequestDto object.</param>
        /// <returns>The Home entity.</returns>
        public static Home ToEntity(this HomeRequestDto homeRequest)
        {
            return new Home
            {
                HomeId = homeRequest.HomeId,
                UserId = homeRequest.UserId,
                MenuId = homeRequest.MenuId,
                Location = homeRequest.Location,
                Size = homeRequest.Size,
                Price = homeRequest.Price,
                PhotoPath = homeRequest.PhotoPath
            };
        }

        /// <summary>
        /// Maps Home entity to HomeRequestDto.
        /// </summary>
        /// <param name="home">The Home entity.</param>
        /// <returns>The HomeRequestDto object.</returns>
        public static HomeRequestDto ToDto(this Home home)
        {
            return new HomeRequestDto
            {
                HomeId = home.HomeId,
                UserId = home.UserId,
                MenuId = home.MenuId,
                Location = home.Location,
                Size = home.Size,
                Price = home.Price,
                PhotoPath = home.PhotoPath
            };
        }

        /// <summary>
        /// Maps MenuRequestDto to Menu entity.
        /// </summary>
        /// <param name="menuRequest">The MenuRequestDto object.</param>
        /// <returns>The Menu entity.</returns>
        public static Menu ToEntity(this MenuRequestDto menuRequest)
        {
            return new Menu
            {
                MenuId = menuRequest.MenuId,
                Name = menuRequest.Name,
                ParentId = menuRequest.ParentId,
                Amblem = menuRequest.Amblem
            };
        }

        /// <summary>
        /// Maps Menu entity to MenuRequestDto.
        /// </summary>
        /// <param name="menu">The Menu entity.</param>
        /// <returns>The MenuRequestDto object.</returns>
        public static MenuRequestDto ToDto(this Menu menu)
        {
            return new MenuRequestDto
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                ParentId = menu.ParentId,
                Amblem = menu.Amblem
            };
        }

        /// <summary>
        /// Maps UserRequestDto to User entity.
        /// </summary>
        /// <param name="userRequest">The UserRequestDto object.</param>
        /// <returns>The User entity.</returns>
        public static User ToEntity(this UserRequestDto userRequest)
        {
            return new User
            {
                UserId = userRequest.UserId,
                Username = userRequest.Username,
                Email = userRequest.Email,
                Password = userRequest.Password
            };
        }

        /// <summary>
        /// Maps User entity to UserRequestDto.
        /// </summary>
        /// <param name="user">The User entity.</param>
        /// <returns>The UserRequestDto object.</returns>
        public static UserRequestDto ToDto(this User user)
        {
            return new UserRequestDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

        /// <summary>
        /// Maps OrderRequestDTO to Order entity.
        /// </summary>
        /// <param name="orderRequest">The OrderRequestDTO object.</param>
        /// <returns>The Order entity.</returns>
        public static Order ToEntity(this OrderRequestDTO orderRequest)
        {
            return new Order
            {
                OrderId = orderRequest.OrderId,
                UserId = orderRequest.UserId,
                ProductType = orderRequest.ProductType,
                MenuId = orderRequest.MenuId
            };
        }

        /// <summary>
        /// Maps Order entity to OrderRequestDTO.
        /// </summary>
        /// <param name="order">The Order entity.</param>
        /// <returns>The OrderRequestDTO object.</returns>
        public static OrderRequestDTO ToDto(this Order order)
        {
            return new OrderRequestDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                ProductType = order.ProductType,
                MenuId = order.MenuId
            };
        }
    }
}
