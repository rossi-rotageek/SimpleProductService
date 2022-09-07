using SimpleService.Entities;

namespace SimpleService.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public Product ToEntity()
        => new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price
        };
    
    public static ProductDto FromEntity(Product product)
        => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
}