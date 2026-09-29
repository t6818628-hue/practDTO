namespace p1_api.DTO
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
    }
}