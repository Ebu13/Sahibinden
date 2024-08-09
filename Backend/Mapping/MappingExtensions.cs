using Backend.Business.Requests;
using Backend.Models;

namespace Backend.Business.Mapping
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps CarRequestDto to Car entity.
        /// </summary>
        public static Car ToEntity(this CarRequestDto carRequest) => new Car
        {
            CarId = carRequest.CarId,
            UserId = carRequest.UserId,
            MenuId = carRequest.MenuId,
            Year = carRequest.Year,
            Price = carRequest.Price,
            PhotoPath = carRequest.PhotoPath
        };

        /// <summary>
        /// Maps Car entity to CarRequestDto.
        /// </summary>
        public static CarRequestDto ToDto(this Car car) => new CarRequestDto
        {
            CarId = car.CarId,
            UserId = car.UserId,
            MenuId = car.MenuId,
            Year = car.Year,
            Price = car.Price,
            PhotoPath = car.PhotoPath
        };

        /// <summary>
        /// Maps HomeRequestDto to Home entity.
        /// </summary>
        public static Home ToEntity(this HomeRequestDto homeRequest) => new Home
        {
            HomeId = homeRequest.HomeId,
            UserId = homeRequest.UserId,
            MenuId = homeRequest.MenuId,
            Location = homeRequest.Location,
            Size = homeRequest.Size,
            Price = homeRequest.Price,
            PhotoPath = homeRequest.PhotoPath
        };

        /// <summary>
        /// Maps Home entity to HomeRequestDto.
        /// </summary>
        public static HomeRequestDto ToDto(this Home home) => new HomeRequestDto
        {
            HomeId = home.HomeId,
            UserId = home.UserId,
            MenuId = home.MenuId,
            Location = home.Location,
            Size = home.Size,
            Price = home.Price,
            PhotoPath = home.PhotoPath
        };

        /// <summary>
        /// Maps MenuRequestDto to Menu entity.
        /// </summary>
        public static Menu ToEntity(this MenuRequestDto menuRequest) => new Menu
        {
            MenuId = menuRequest.MenuId,
            Name = menuRequest.Name,
            ParentId = menuRequest.ParentId,
            Amblem = menuRequest.Amblem
        };

        /// <summary>
        /// Maps Menu entity to MenuRequestDto.
        /// </summary>
        public static MenuRequestDto ToDto(this Menu menu) => new MenuRequestDto
        {
            MenuId = menu.MenuId,
            Name = menu.Name,
            ParentId = menu.ParentId,
            Amblem = menu.Amblem
        };

        /// <summary>
        /// Maps UserRequestDto to User entity.
        /// </summary>
        public static User ToEntity(this UserRequestDto userRequest) => new User
        {
            UserId = userRequest.UserId,
            Username = userRequest.Username,
            Email = userRequest.Email,
            Password = userRequest.Password,
            Role = userRequest.Role
        };

        /// <summary>
        /// Maps User entity to UserRequestDto.
        /// </summary>
        public static UserRequestDto ToDto(this User user) => new UserRequestDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        };

        /// <summary>
        /// Maps OrderRequestDTO to Order entity.
        /// </summary>
        public static Order ToEntity(this OrderRequestDTO orderRequest) => new Order
        {
            OrderId = orderRequest.OrderId,
            UserId = orderRequest.UserId,
            ProductType = orderRequest.ProductType,
            MenuId = orderRequest.MenuId
        };

        /// <summary>
        /// Maps Order entity to OrderRequestDTO.
        /// </summary>
        public static OrderRequestDTO ToDto(this Order order) => new OrderRequestDTO
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            ProductType = order.ProductType,
            MenuId = order.MenuId
        };
    }
}
