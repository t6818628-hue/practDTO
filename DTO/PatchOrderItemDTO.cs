namespace p1_api.DTO
{
    public class PatchOrderItemDTO
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceAtTime { get; set; }
    }
}