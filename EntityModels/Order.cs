using System;
using System.Collections.Generic;

namespace stepmedia_demo.EntityModels;

public partial class Order
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public long ShopId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Shop Shop { get; set; } = null!;
}
