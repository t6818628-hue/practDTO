namespace p1_api.DTO
{
    public class UpdateCartItemDTO
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}