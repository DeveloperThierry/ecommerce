using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using app.DTOs;
using app.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetCurrentUserId();
            var cart = await cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await cartService.AddToCartAsync(userId, dto);
            return Ok(cart);
        } 

        [HttpPut("items/{productId}")]
        public async Task<IActionResult> UpdateCartItem(int productId, AddToCartDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await cartService.UpdateCartItemAsync(userId, productId, dto);
            if(cart is null) return NotFound();
            return Ok(cart);
        } 

        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> DeleteCartItem(int productId)
        {
            var userId = GetCurrentUserId();
            var removed = await cartService.RemoveFromCartAsync(userId, productId);
            if(!removed) return NotFound();
            return NoContent();
        } 

        private int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId!);

        }
    }
}