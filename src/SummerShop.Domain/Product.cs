using SummerShop.Domain.Entities;

namespace SummerShop.WebApi.Domain;

public class Product :Entity
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt => DateTime.Today;
}