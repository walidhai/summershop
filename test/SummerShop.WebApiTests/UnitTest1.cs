using SummerShop.Domain.Entities;
using SummerShop.WebApi.Domain;
//using FluentAssertion;
namespace SummerShop.WebApiTests;

public class UnitTest1
{
    [Fact]
    public void AddProductToCart_WithValidProduct_IsSuccesful()
    {
        var product = new Product{ProductName = "Testproduct", Id = 1, Price = 100};
        //var cartItem = new CartItem(1, 1);
        var cartItem = new CartItem { ProductId = 1, ItemId = 1 };
        var cart = new Cart();
        cart.AddProductToCart(cartItem);
        Assert.Equal(cartItem, cart.CartItems[0]);
    }
}