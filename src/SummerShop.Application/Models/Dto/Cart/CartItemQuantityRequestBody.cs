using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SummerShop.Application.Models.Dto.Cart;

public class CartItemQuantityRequestBody
{
    [Required]
    [JsonPropertyName("cartId")]
    public int CartId { get; set; }
    
    [Required]
    [JsonPropertyName("itemId")]
    public int ItemId { get; set; }
    
    [Required]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}