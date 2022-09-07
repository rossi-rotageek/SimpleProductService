using System.ComponentModel.DataAnnotations;
using SimpleService.Dtos;

namespace SimpleService.Models;

public class CreateProductModel
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Price cannot be less than 1")]
    public decimal Price { get; set; }

    public ProductDto ToDto()
        => new ProductDto
        {
            Name = Name,
            Description = Description,
            Price = Price
        };
}