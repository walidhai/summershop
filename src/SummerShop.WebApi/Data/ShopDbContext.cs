using Microsoft.EntityFrameworkCore;
using SummerShop.WebApi.Domain;

namespace SummerShop.WebApi.Data;

public class ShopDbContext:DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
}