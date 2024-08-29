using System.ComponentModel.DataAnnotations;
using SummerShop.WebApi.Domain;

namespace SummerShop.Domain.Entities;

public class Cart : Entity
{
    public Cart()
    {
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
