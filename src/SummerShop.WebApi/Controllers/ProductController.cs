using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerShop.WebApi.Data;
using SummerShop.WebApi.Domain;
using SummerShop.WebApi.Models;
using SummerShop.WebApi.Models.Mappings;

namespace SummerShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ShopDbContext _context;
    // GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetArticle(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Product>> PostArticle(AddProductModel addProductModel)
    {
        try
        {
            var product = addProductModel.ToProduct();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = product.Id }, product);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}