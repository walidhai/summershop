using SummerShop.Application.Models.Dto.Cart;
using SummerShop.Application.Models.Dto.CartItem;
using SummerShop.Domain.Entities;
using SummerShop.WebApi.Domain;

namespace SummerShop.Application.Models.Mappings;

public static class CartMapping
{
    public static Cart CartFromCreateCartDto(this CartCreationRequestDto creationRequestDto, List<Product> products)
    {
        return new Cart
        {
            CartItems = creationRequestDto.Products.ToCartItemsList(products)
        };
    }

    public static List<CartItem> ToCartItemsList(this List<CartItemRequestDto> cartItemRequestDtos, List<Product> products)
    {
        if (cartItemRequestDtos is null)
            return null;
        if (products.Count is 0)
            return null;
        var carItemList = cartItemRequestDtos.Select(item => new CartItem
            {
                //CartId = id,
                Product = products.FirstOrDefault(x => x.Id == item.ItemId),
                Quantity = item.Quantity
            })
            .ToList();
        
        return carItemList.Count is 0 ? null : carItemList;
    }
}