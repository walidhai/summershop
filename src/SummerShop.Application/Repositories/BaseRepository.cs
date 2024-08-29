using Microsoft.EntityFrameworkCore;
using SummerShop.Data;
using SummerShop.Domain.Entities;

namespace SummerShop.Application.Repositories;

public abstract class BaseRepository<T> where T: Entity
{
    private readonly ShopDbContext _dbContext;

    protected BaseRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual async Task<T> FindByIdAsync(int id) => await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    public virtual async Task<List<T>> FetchAllAsync() => await _dbContext.Set<T>().ToListAsync();

    public virtual async Task<List<T>> FetchListById(List<int> ids) => await _dbContext.Set<T>()
        .Where(item => ids.Contains(item.Id))
        .ToListAsync();

}

public interface IBaseRepository<T> where T : Entity
{
    Task<T> FindByIdAsync(int id);
    Task<List<T>> FetchAllAsync();
    Task<List<T>> FetchListById(List<int> ids);
}