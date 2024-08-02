using Backend.Models;

namespace Backend.Business.Requests
{
    public class MenuRequestDto
    {
        public int MenuId { get; set; }

        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }

        public string? Amblem { get; set; }

    }
}
