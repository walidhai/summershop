using System.ComponentModel.DataAnnotations;
using SummerShop.WebApi.Domain;

namespace SummerShop.Domain.Entities;

public class CartItem
{
    //Maybe make these ids guid instead of ints - will pass in an id int for now for simplicty sake
    public CartItem(int itemId, int productId)
    {
        ItemId = itemId;
        ProductId = productId;
        Quantity = 1;
    }
    public int CartId { get; init; }
    [Key]
    public int ItemId { get; init; }
    public Product Product { get; set; } 
    public int Quantity { get; set; }
    public int ProductId { get; init; }

    public void AddQuantity(int n)
    {
        if (n > 999 || (Quantity + n) > 999)
            return;
        Quantity += n;
    }

    public void SubstractQuantity(int n)
    {
        //remove item if 0
        if ((Quantity - 1) == 0)
            return;
        //return error
        if(Quantity-1 <0)
            return;
        Quantity -= n;
    }
}