using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCart(string id)
        {
            ShoppingCart? cart = await cartService.GetCartAsync(id);

            return Ok(cart ?? new ShoppingCart { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            ShoppingCart? updatedCart = await cartService.SetCartAsync(cart);
           
            if (updatedCart == null) return BadRequest("Problem updating the cart");

            return updatedCart;

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string id)
        {
            var result = await cartService.DeleteCartAsync(id);

            if (!result)
            {
                BadRequest("Problem deleting the cart");
            }
            return Ok();
        }
    }
}
