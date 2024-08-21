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
        var cart = new Cart(1);
        cart.AddProductToCart(product);
        Assert.Equal(product, cart.Products[0]);
    }
}