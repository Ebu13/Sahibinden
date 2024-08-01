using Backend.Models;

namespace Backend.Business.Requests
{
    public class HomeRequestDto
    {
        public int HomeId { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }

        public string Location { get; set; } = null!;

        public int Size { get; set; }

        public decimal Price { get; set; }

        public string? PhotoPath { get; set; }

    }
}
