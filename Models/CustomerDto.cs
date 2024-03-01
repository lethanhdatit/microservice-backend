using stepmedia_demo.Models;
using System.ComponentModel.DataAnnotations;

namespace stepmedia_demo.EntityModels;

public class CustomerDto
{
    public long Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Dob { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class CustomerCreation
{
    [Required]
    [StringLength(50)]
    public string FullName { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    public string Dob { get; set; }
}

public class CustomerResponse : BaseResponse
{
}
