using stepmedia_demo.Models;
using System.ComponentModel.DataAnnotations;

namespace stepmedia_demo.EntityModels;

public class OrderDetailDto
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public long OrderId { get; set; }

    public double UnitPrice { get; set; }

    public byte Quantity { get; set; }
}

public class OrderDetailCreation
{
    [Required]
    public long ProductId { get; set; }

    public long OrderId { get; set; }

    [Required]
    public double UnitPrice { get; set; }

    [Required]
    public byte Quantity { get; set; }
}

public class OrderDetailResponse : BaseResponse
{
}
