using stepmedia_demo.Models;
using System.ComponentModel.DataAnnotations;

namespace stepmedia_demo.EntityModels;

public class OrderDto
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public long ShopId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string ShopName { get; set; } = string.Empty;

    public double Total { get { return Items.Sum(s => s.Quantity * s.UnitPrice); } }

    public List<OrderDetailDto> Items { get; set; }
}

public class OrderCreation
{
    [Required]
    public long CustomerId { get; set; }

    [Required]
    public long ShopId { get; set; }

    [Required]
    public List<OrderDetailCreation> Items { get; set; }
}

public class OrderResponse : BaseResponse
{
}