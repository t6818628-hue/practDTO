using System;

namespace p1_api.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
    }
}