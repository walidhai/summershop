using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SummerShop.Domain.Entities;
using SummerShop.WebApi.Domain;

namespace SummerShop.Data;

public class ShopDbContext:DbContext
{
    protected readonly IConfiguration _configuration;
    public ShopDbContext(DbContextOptions<ShopDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));   
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
}