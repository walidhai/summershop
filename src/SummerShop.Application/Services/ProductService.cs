using SummerShop.Application.Models;
using SummerShop.Application.Models.Mappings;
using SummerShop.Data;
using SummerShop.WebApi.Domain;

namespace SummerShop.Application.Services;

public class ProductService(ShopDbContext context) : IProductService
{
    public async Task<Product?> TryAddProduct(AddProductModel addProductModel)
    {
        try
        {
            var product = addProductModel.ToProduct();

            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
        catch (Exception e)
        {
            //Handle exception
            Console.WriteLine(e);
            return null;
        }
    }
}

public interface IProductService
{
    public Task<Product?> TryAddProduct(AddProductModel addProductModel);
}