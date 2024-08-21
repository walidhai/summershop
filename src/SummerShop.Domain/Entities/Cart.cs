using System.ComponentModel.DataAnnotations;
using SummerShop.WebApi.Domain;

namespace SummerShop.Domain.Entities;

public class Cart
{
    public Cart(int id)
    {
        Id = id;
        CartItems = new List<CartItem>();
    }
    [Key]
    public int Id { get; init; }
    public List<CartItem> CartItems { get; set; }
    

    public void AddProductToCart(CartItem item)
    {
        CartItems.Add(item);
    }
    
}
