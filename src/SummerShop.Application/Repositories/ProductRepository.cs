using SummerShop.Data;
using SummerShop.WebApi.Domain;

namespace SummerShop.Application.Repositories;

public class ProductRepository: BaseRepository<Product>, IProductRepository
{
    private readonly ShopDbContext _dbContext;
    public ProductRepository(ShopDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await FindByIdAsync(id);
    }

    public async Task<List<Product>> GetListOfProducts(List<int> ids)
    {
        return await FetchListById(ids);
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

public interface IProductRepository: IBaseRepository<Product>
{
    public Task<Product?> GetProductAsync(int id);
    public Task<List<Product>> GetListOfProducts(List<int> ids);
}