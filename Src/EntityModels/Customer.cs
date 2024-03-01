using System;
using System.Collections.Generic;

namespace stepmedia_demo.EntityModels;

public partial class Customer
{
    public long Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public DateTime? DoB { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
