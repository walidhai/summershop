using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SummerShop.Application.Models.Dto.CartItem;

public class CartItemRequestDto
{
    [JsonPropertyName("ItemId")]
    [Required]
    public int ItemId { get; set; }
    [JsonPropertyName("Quantity")]
    [Required]
    public int Quantity { get; set; }
}