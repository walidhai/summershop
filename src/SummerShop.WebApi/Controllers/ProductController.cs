using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerShop.Application.Models;
using SummerShop.Application.Models.Mappings;
using SummerShop.Application.Services;
using SummerShop.Data;
using SummerShop.WebApi.Domain;

namespace SummerShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly ShopDbContext _context;
    // GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(AddProductModel addProductModel)
    {
        try
        {
            var product = await productService.TryAddProduct(addProductModel);
            //returnere noe bedre enn null om det failer
            if (product is null)
                return BadRequest();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}