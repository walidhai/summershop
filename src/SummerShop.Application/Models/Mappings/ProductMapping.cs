
using SummerShop.WebApi.Domain;

namespace SummerShop.Application.Models.Mappings;

public static class ProductMapping
{
    public static Product ToProduct(this AddProductModel productModel)
    {
        return new Product
        {
            ProductName = productModel.ProductName,
            Price = productModel.Price
        };
    }

    public static Product ToProduct(this UpdateProductModel productModel)
    {
        return new Product
        {
            ProductName = productModel.ProductName,
            Price = productModel.Price
        };
    }

    public static GetProductModel FromProduct(this Product product)
    {
        return new GetProductModel
        {
            ProductName = product.ProductName,
            Price = product.Price
        };
    }
}