namespace Backend.Business.Requests
{
    public class OrderRequestDTO
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string ProductType { get; set; } = null!;

        public int MenuId { get; set; }
    }
}
