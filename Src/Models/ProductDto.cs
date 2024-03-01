using stepmedia_demo.Models;
using System.ComponentModel.DataAnnotations;

namespace stepmedia_demo.EntityModels;

public class ProductDto
{
    public long Id { get; set; }

    public long ShopId { get; set; }

    public string Name { get; set; } = null!;

    public string ShopName { get; set; } = null!;

    public double UnitPrice { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class ProductCreation
{
    [Required]
    public long ShopId { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = null!;

    [Required]
    public double UnitPrice { get; set; }
}

public class ProductResponse : BaseResponse
{
}