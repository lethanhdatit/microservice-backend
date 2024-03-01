using System;
using System.Collections.Generic;

namespace stepmedia_demo.EntityModels;

public partial class OrderDetail
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long OrderId { get; set; }

    public double UnitPrice { get; set; }

    public byte Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
