using System;

namespace p1_api.DTO
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}