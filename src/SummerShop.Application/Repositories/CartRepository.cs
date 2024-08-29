using SummerShop.Application.Models.Dto.Cart;
using SummerShop.Application.Models.Mappings;
using SummerShop.Data;
using SummerShop.Domain.Entities;

namespace SummerShop.Application.Repositories;

public class CartRepository: BaseRepository<Cart>, ICartRepository
{
    private readonly ShopDbContext _dbContext;
    private readonly IProductRepository _productRepository;
    private readonly ICartItemRepository _cartItemRepository;
    
    public CartRepository(ShopDbContext dbContext, IProductRepository productRepository, ICartItemRepository cartItemRepository) : base(dbContext)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task<Cart?> GetCartAsync(int cartId)
    {
        return await FindByIdAsync(cartId);
    }

    public async Task<Cart?> CreateShoppingCartAsync()
    {
        //if (cartContentsRequest.Products.Count == 0)
            //return null;
        //map from dto to cart
        //might be a bug in creating and relating products via productId
        //var cart = cartContentsRequest.CartFromCreateCartDto();
        //if (cart is null)
            //return null;
        var cart = new Cart();
        _dbContext.Add(cart);
        await _dbContext.SaveChangesAsync();
        return await GetCartAsync(cart.Id);
    }

    public async Task<Cart?> UpdateShoppingCartAsync(int cartId, CartUpdateRequestDto cartContentsRequest)
    {
        if (cartContentsRequest.Products.Count is 0)
            return null;
        var cart = await GetCartAsync(cartId);
        if (cart is null)
            return null;
        var cartItems = await _cartItemRepository.GetListOfCartItemsForCart(cartId);
        cart.CartItems = cartItems;
        var itemIds = cartContentsRequest.Products
            .Select(x => x.ItemId)
            .Except(cartItems.Select(p => p.ProductId))
            .ToList();
        if (itemIds.Count is 0)
            return cart;
        var products = await _productRepository.GetListOfProducts(itemIds);
        cart.CartItems = cartContentsRequest.Products.ToCartItemsList(products);
        _dbContext.Update(cart);
        await _dbContext.SaveChangesAsync();
        return cart;
    }

    public async Task RemoveShoppingCartAsync(int cartId)
    {
        var cart = await GetCartAsync(cartId);
        if(cart is null)
            throw new NotImplementedException();
        _dbContext.Remove(cart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Cart> RemoveShoppingCartItemAsync(int cartId, int itemId)
    {
        //change later to return errors rather than null
        var cart = await GetCartAsync(cartId);
        if (cart is null)
            return null;
        var product = await _productRepository.GetProductAsync(itemId);
        if (product is null)
            return null;
        var removeableItem = cart.CartItems.FirstOrDefault(x => x.ProductId == itemId);
        if (removeableItem is null)
            return null;
        cart.CartItems.Remove(removeableItem);
        await _dbContext.SaveChangesAsync();
        return await GetCartAsync(cartId);
    }

    public async Task<Cart?> IncreaseShoppingCartItemAsync(int cartId, int itemId, int quantity)
    {
        var cart = await FindByIdAsync(cartId);
        if (cart is null)
            return null;
        var product = await _productRepository.GetProductAsync(itemId);
        if (product is null)
            return null;
        if (cart.CartItems.Any(x => x.ProductId == itemId))
        {
            var item = cart.CartItems.First(x => x.ProductId == itemId);
            item.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                CartId = cartId,
                ItemId = itemId,
                Quantity = quantity
            });
        }

        _dbContext.Attach(cart);
        await _dbContext.SaveChangesAsync();
        //FInd a better return body
        return await GetCartAsync(cartId);
    }

    public async Task<Cart> DecreaseShoppingCartItemAsync(int cartId, int itemId, int quantity)
    {
        var cart = await FindByIdAsync(cartId);
        if (cart is null)
            return null;
        var product = await _productRepository.GetProductAsync(itemId);
        if (product is null)
            return null;
        if (!cart.CartItems.Any(x => x.ProductId == itemId))
            return null;
        var item = cart.CartItems.First(x => x.ProductId == itemId);
        item.Quantity -= quantity;
        if (quantity < 0)
            return await RemoveShoppingCartItemAsync(cartId, itemId);
        _dbContext.Attach(cart);
        await _dbContext.SaveChangesAsync();
        return await GetCartAsync(cartId);
    }

    public async Task<Cart> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<Cart?> GetCartAsync(int cartId);
    Task<Cart> CreateShoppingCartAsync();
    Task<Cart> UpdateShoppingCartAsync(int cartId, CartUpdateRequestDto cartContentsRequest);
    Task RemoveShoppingCartAsync(int cartId);
    Task<Cart> RemoveShoppingCartItemAsync(int cartId, int itemId);
    Task<Cart> IncreaseShoppingCartItemAsync(int cartId, int itemId, int quantity);
    Task<Cart> DecreaseShoppingCartItemAsync(int cartId, int itemId, int quantity);
}