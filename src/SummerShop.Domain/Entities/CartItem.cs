using System.ComponentModel.DataAnnotations;
using SummerShop.WebApi.Domain;

namespace SummerShop.Domain.Entities;

public class CartItem : Entity
{
    //Maybe make these ids guid instead of ints - will pass in an id int for now for simplicty sake
    /*public CartItem(int itemId, int productId)
    {
        ItemId = itemId;
        ProductId = productId;
        Quantity = 1;
    }*/
    [Key]
    public int ItemId { get; init; }
    public Product? Product { get; set; } 
    public int Quantity { get; set; }
    public int ProductId { get; init; }
    public int CartId { get; set; }
}