using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerShop.Application.Models;
using SummerShop.Application.Models.Dto;
using SummerShop.Application.Models.Mappings;
using SummerShop.Application.Services;
using SummerShop.Data;
using SummerShop.WebApi.Domain;

namespace SummerShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetProductModel>>> GetProducts()
    {
        var allProducts = await productService.TryGetAllProducts();
        return allProducts is null ? NotFound() : Ok(allProducts);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productService.TryGetProduct(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(AddProductModel addProductModel)
    {
        try
        {
            var product = await productService.TryAddProduct(addProductModel);
            //returnere noe bedre enn null om det failer
            return product is null
                ? BadRequest()
                : CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}