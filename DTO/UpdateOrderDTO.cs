namespace p1_api.DTO
{
    public class UpdateOrderDTO
    {
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
    }
}