using Microsoft.Extensions.Logging;
using SummerShop.Application.Models.Dto.Cart;
using SummerShop.Application.Repositories;
using SummerShop.Data;
using SummerShop.Domain.Entities;

namespace SummerShop.Application.Services;

public class CartService(ILogger<CartService> logger, ICartRepository cartRepository) : ICartService
{
    public async Task<Cart?> GetCart(int cartId)
    {
        //Extend with login
        //var loggedIn = false;
        //if(loggedIn)
        var cart = await cartRepository.GetCartAsync(cartId);
        return cart ?? null;
    }

    public async Task<Cart?> CreateCartIfNotExist(int cartId)
    {
        var cart = await cartRepository.GetCartAsync(cartId);
        if (cart is not null)
            return cart;
        return await cartRepository.CreateShoppingCartAsync() ?? null;
    }
    
    public async Task<Cart?> AddItemToCart(int cartId, CartUpdateRequestDto cartUpdateRequestDto)
    {
        if (cartUpdateRequestDto is null)
            throw new Exception("There are no carts to use.");
        var cart = new Cart();
        try
        {
            cart = await cartRepository.UpdateShoppingCartAsync(cartId, cartUpdateRequestDto);
        }
        catch (Exception e)
        {
            logger.LogError("Failed to update cart in database. Reason {reason}", e.Message);
            throw new Exception();
        }

        return cart;
    }

    public async Task<Cart?> IncreaseQuantityOfItem(int cartId, int itemId, int quantity)
    {
        try
        {
            return await cartRepository.IncreaseShoppingCartItemAsync(cartId, itemId, quantity);
        }
        catch (Exception e)
        {
            logger.LogError("Couldnt increase quantity of item. Reason {reason}", e.Message);
            return null;
        }
    }

    public async Task<Cart?> DecreaseQuantityOfItem(int cartId, int itemId, int quantity)
    {
        try
        {
            return await cartRepository.DecreaseShoppingCartItemAsync(cartId, itemId, quantity);
        }
        catch (Exception e)
        {
            logger.LogError("Couldnt decrease quantity of item. Reason {reason}", e.Message);
            return null;
        }
    }
}

public interface ICartService
{
    public Task<Cart?> GetCart(int cartId);
    public Task<Cart?> CreateCartIfNotExist(int cartId);
    public Task<Cart?> AddItemToCart(int cartId, CartUpdateRequestDto cartUpdateRequestDto);
    public Task<Cart?> IncreaseQuantityOfItem(int cartId, int itemId, int quantity);
    public Task<Cart?> DecreaseQuantityOfItem(int cartId, int itemId, int quantity);
}