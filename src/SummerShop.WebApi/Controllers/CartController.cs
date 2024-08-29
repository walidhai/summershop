using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerShop.Application.Extensions;
using SummerShop.Application.Models.Dto.Cart;
using SummerShop.Application.Services;
using SummerShop.Data;
using SummerShop.Domain.Entities;

namespace SummerShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {

        // GET: api/Cart
        //[Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            var cart = await cartService.GetCart(1);
            return cart == null ? NotFound() : Ok(cart);
        }

        // GET: api/Cart/5
        //[Authorize(Roles="Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await cartService.GetCart(id);
            return cart == null ? NotFound() : Ok(cart);
        }
        // GET: api/Cart/5
        //[Authorize(Roles="Admin")]
        [HttpGet("/create/{id}")]
        public async Task<ActionResult<Cart>> GetOrCreateCart(int id)
        {
            var cart = await cartService.CreateCartIfNotExist(id);
            return cart == null ? NotFound() : Ok(cart);
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, [FromBody] CartUpdateRequestDto cartRequestObject)
        {
            if (cartRequestObject.Products.Count is 0)
                return BadRequest();
            /*var cartObject = new CartUpdateRequestDto();
            try
            {
                //cartObject = cartJson.FromJson<CartUpdateRequestDto>();
                if (cartObject is null)
                    return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error in json transformation {e.Message}");
                return BadRequest();
            }*/

            var updateCart = await cartService.AddItemToCart(id, cartRequestObject);
            return updateCart is null ? BadRequest() : Ok(updateCart);
        }

        // POST: api/Cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/decrease")]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> IncreaseCartItemQuantity([FromBody] CartItemQuantityRequestBody quantityRequest)
        {
            if (quantityRequest.Quantity is 0)
                return BadRequest();
            try
            {
                var updateCartItemQuantity = await cartService.IncreaseQuantityOfItem(quantityRequest.CartId,
                    quantityRequest.ItemId, quantityRequest.Quantity);
                return updateCartItemQuantity is null ? BadRequest() : Ok(updateCartItemQuantity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost("/api/decrease")]
        public async Task<ActionResult<Cart>> DecreaseCartItemQuantity([FromBody] CartItemQuantityRequestBody quantityRequest)
        {
            if (quantityRequest.Quantity is 0)
                return BadRequest();
            try
            {
                var updateCartItemQuantity = await cartService.DecreaseQuantityOfItem(quantityRequest.CartId,
                    quantityRequest.ItemId, quantityRequest.Quantity);
                return updateCartItemQuantity is null ? BadRequest() : Ok(updateCartItemQuantity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            return NoContent();
        }

        private bool CartExists(int id)
        {
            return true;
        }
    }
}
