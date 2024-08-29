using Microsoft.EntityFrameworkCore;
using SummerShop.Data;
using SummerShop.Domain.Entities;

namespace SummerShop.Application.Repositories;

public class CartItemRepository: BaseRepository<CartItem>, ICartItemRepository
{
    private readonly ShopDbContext _dbContext;
    public CartItemRepository(ShopDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CartItem?> GetProductAsync(int id)
    {
        return await FindByIdAsync(id);
    }

    public async Task<List<CartItem>> GetListOfCartItemsForCart(int cartId)
    {
        return await _dbContext.Set<CartItem>()
            .Where(item => item.CartId == cartId)
            .ToListAsync();
    }

    public async Task<CartItem> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
public interface ICartItemRepository: IBaseRepository<CartItem>
{
    public Task<CartItem?> GetProductAsync(int id);
    public Task<List<CartItem>> GetListOfCartItemsForCart(int cartId);
}