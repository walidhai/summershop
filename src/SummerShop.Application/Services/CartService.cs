using SummerShop.Data;
using SummerShop.Domain.Entities;

namespace SummerShop.Application.Services;

public class CartService(ShopDbContext dbContext) : ICartService
{
    public async Task<Cart> GetCart()
    {
        var loggedIn = false;
        return  loggedIn ?  await dbContext.Carts.FindAsync(1) : new Cart(1);
    }

    public void AddItemToCart(CartItem item)
    {
        throw new NotImplementedException();
    }
}

public interface ICartService
{
    public Task<Cart> GetCart();
    public void AddItemToCart(CartItem item);
}