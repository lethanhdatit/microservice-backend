using System;
using System.Collections.Generic;

namespace stepmedia_demo.EntityModels;

public partial class Product
{
    public long Id { get; set; }

    public long ShopId { get; set; }

    public string Name { get; set; } = null!;

    public double UnitPrice { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Shop Shop { get; set; } = null!;
}
