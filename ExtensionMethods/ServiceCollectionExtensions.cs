using SimpleService.BL;
using SimpleService.DataServices;

namespace SimpleService.ExtensionMethods;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductManager, ProductManager>();
    }
}