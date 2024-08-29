using System.Text.Json.Serialization;
using SummerShop.Application.Models.Dto.CartItem;

namespace SummerShop.Application.Models.Dto.Cart;

public class CartUpdateRequestDto
{
    [JsonPropertyName("Products")]
    public List<CartItemRequestDto> Products { get; set; } = new List<CartItemRequestDto>();
}