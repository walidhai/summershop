namespace SummerShop.WebApi.Data;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt => DateTime.Today;
}