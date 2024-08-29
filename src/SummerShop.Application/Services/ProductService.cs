using System.Collections;
using Microsoft.EntityFrameworkCore;
using SummerShop.Application.Models;
using SummerShop.Application.Models.Dto;
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

    public async Task<GetProductModel?> TryGetProduct(int id)
    {
        try
        {
            var product = await context.Products.FindAsync(id);
            if (product is null)
                return null;
            return product.FromProduct();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<GetProductModel>?> TryGetAllProducts()
    {
        var products = await  context.Set<Product>().ToListAsync();
        return products.Select(product => product.FromProduct());
    }
}

public interface IProductService
{
    public Task<Product?> TryAddProduct(AddProductModel addProductModel);
    public Task<GetProductModel?> TryGetProduct(int id);
    public Task<IEnumerable<GetProductModel>?> TryGetAllProducts();
}