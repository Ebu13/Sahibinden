using Backend.Business.Requests;
using Backend.Models;

namespace Backend.Business.Mapping
{
    public static class MappingExtensions
    {
        // Car Mapping
        public static Car ToEntity(this CarRequestDto carRequest)
        {
            return new Car
            {
                CarId = carRequest.CarId,
                UserId = carRequest.UserId,
                MenuId = carRequest.MenuId,
                Year = carRequest.Year,
                Price = carRequest.Price
            };
        }

        public static CarRequestDto ToDto(this Car car)
        {
            return new CarRequestDto
            {
                CarId = car.CarId,
                UserId = car.UserId,
                MenuId = car.MenuId,
                Year = car.Year,
                Price = car.Price
            };
        }

        // Home Mapping
        public static Home ToEntity(this HomeRequestDto homeRequest)
        {
            return new Home
            {
                HomeId = homeRequest.HomeId,
                UserId = homeRequest.UserId,
                MenuId = homeRequest.MenuId,
                Location = homeRequest.Location,
                Size = homeRequest.Size,
                Price = homeRequest.Price
            };
        }

        public static HomeRequestDto ToDto(this Home home)
        {
            return new HomeRequestDto
            {
                HomeId = home.HomeId,
                UserId = home.UserId,
                MenuId = home.MenuId,
                Location = home.Location,
                Size = home.Size,
                Price = home.Price
            };
        }

        // Menu Mapping
        public static Menu ToEntity(this MenuRequestDto menuRequest)
        {
            return new Menu
            {
                MenuId = menuRequest.MenuId,
                Name = menuRequest.Name,
                ParentId = menuRequest.ParentId
            };
        }

        public static MenuRequestDto ToDto(this Menu menu)
        {
            return new MenuRequestDto
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                ParentId = menu.ParentId
            };
        }

        // User Mapping
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
    }
}
