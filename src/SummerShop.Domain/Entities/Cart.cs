using SummerShop.WebApi.Domain;

namespace SummerShop.Domain.Entities;

public class Cart
{
    public Cart(int id)
    {
        Id = id;
        Products = new List<Product>();
    }
    public int Id { get; init; }
    public List<Product> Products { get; set; }
    

    public void AddProductToCart(Product product)
    {
        Products.Add(product);
    }

    
}
