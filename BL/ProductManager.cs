using SimpleService.Core;
using SimpleService.DataServices;
using SimpleService.Dtos;

namespace SimpleService.BL;

public interface IProductManager
{
    Task<IList<ProductDto>> ListProducts(string searchTerms);
    Task<Result<ProductDto>> GetProductById(int id);
    Task<Result> DeleteProduct(int id);
    Task<Result> UpdateProduct(ProductDto dto);
    Task<ProductDto> CreateProduct(ProductDto dto);
}


public class ProductManager : IProductManager
{
    private readonly IProductService _productService;

    public ProductManager(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IList<ProductDto>> ListProducts(string searchTerms)
    {
        var products = await _productService.ListProducts(searchTerms.ToLowerInvariant());
        return products.Select(ProductDto.FromEntity).ToList();
    }

    public async Task<Result<ProductDto>> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        
        return product == null 
            ? Result<ProductDto>.Failure(ErrorType.ValueNotFound, "Product not found") 
            : Result<ProductDto>.Success(ProductDto.FromEntity(product));
    }

    public async Task<Result> DeleteProduct(int id)
    {
        var product = await _productService.GetProductById(id);

        if (product == null)
            return Result.Failure(ErrorType.ValueNotFound, "Product not found");
        
        await _productService.DeleteProduct(product);
        return Result.Success();
    }

    public async Task<Result> UpdateProduct(ProductDto dto)
    {
        var product = await _productService.GetProductById(dto.Id);

        if (product == null)
            return Result<ProductDto>.Failure(ErrorType.ValueNotFound, "Product not found");
        
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        
        await _productService.UpdateProduct(product);
        
        return Result.Success();
    }

    public async Task<ProductDto> CreateProduct(ProductDto dto)
    {
        var product = await _productService.CreateProduct(dto.ToEntity());
        return ProductDto.FromEntity(product);
    }
}