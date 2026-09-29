using System;
using System.Collections.Generic;

namespace p1_api.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceAtTime { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
