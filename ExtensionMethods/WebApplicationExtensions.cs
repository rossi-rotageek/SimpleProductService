using SimpleService.DataServices;
using SimpleService.Entities;
using SimpleService.Middlewares;

namespace SimpleService.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static void FeedSampleData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

        var productOne = new Product
        {
            Id = 1,
            Name = "Fridge",
            Description = "Best fridge ever",
            Price = 3000
        };
        
        var productTwo =  new Product
        {
            Id = 2,
            Name = "Television",
            Description = "Best TV ever",
            Price = 2000
        };
        
        dbContext.Products.AddRange(new List<Product>
        {
            productOne,
            productTwo
        });
        
        dbContext.SaveChanges();
    }
    
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>(); 
    }
}