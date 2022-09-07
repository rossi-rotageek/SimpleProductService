using Microsoft.EntityFrameworkCore;
using SimpleService.Entities;

namespace SimpleService.DataServices;

public interface IProductService
{
    Task<IList<Product>> ListProducts(string searchTerms);
    Task<Product?> GetProductById(int id);
    Task DeleteProduct(Product product);
    Task UpdateProduct(Product product);
    Task<Product> CreateProduct(Product product);
}

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Product>> ListProducts(string searchTerms)
        => await _dbContext.Products.Where(p =>
                p.Name.ToLowerInvariant().Contains(searchTerms) ||
                p.Description.ToLowerInvariant().Contains(searchTerms))
            .ToListAsync();
    public async Task<Product?> GetProductById(int id)
        => await _dbContext.Products.Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    
    public async Task DeleteProduct(Product product)
    {
        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        _dbContext.Attach(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Product> CreateProduct(Product product)
    {
        await _dbContext.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }
}