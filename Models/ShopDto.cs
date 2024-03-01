using stepmedia_demo.Models;
using System.ComponentModel.DataAnnotations;

namespace stepmedia_demo.EntityModels;

public class ShopDto
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}

public class ShopCreation
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Location { get; set; } = null!;
}

public class ShopResponse : BaseResponse
{
}