using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerShop.WebApi.Data;
using SummerShop.WebApi.Models.Product;

namespace SummerShop.WebApi.Controllers;

[Route("API/[controller]")]
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

        return product;
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Product>> PostArticle(AddProductModel addProductModel)
    {
        try
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddProductModel, Product>());
            var mapper = config.CreateMapper();

            var product = mapper.Map<Product>(addProductModel);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = product.Id }, product);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}