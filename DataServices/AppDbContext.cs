using Microsoft.EntityFrameworkCore;
using SimpleService.Entities;

namespace SimpleService.DataServices;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> 
        options):base(options)
    { }
    
    public DbSet<Product> Products { get; set; }
    
}