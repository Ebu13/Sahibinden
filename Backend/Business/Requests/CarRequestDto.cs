using Backend.Models;

namespace Backend.Business.Requests
{
    public class CarRequestDto
    {
        public int CarId { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public string? PhotoPath { get; set; }

    }
}
