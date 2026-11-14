using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.DTOs;
using app.Models;
using app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class CartService(AppDbContext context):ICartService
    {
        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            return new CartDto()
            {
                CartId=cart.Id,
                Items = cart.Items.Select(i=> new CartItemDto()
                {
                    ProductId=i.ProductId,
                    Quantity=i.Quantity,
                    ProductName=i.Product.Name,
                    Price=i.Product.Price

                }).ToList()
            };
        }
        public async Task<CartDto> AddToCartAsync(int userId, AddToCartDto dto)
        {
            var productExists = await context.Products.AnyAsync(p => p.Id == dto.ProductId);
            var existingProduct = await context.Products.FindAsync(dto.ProductId);
            if (!productExists || existingProduct is null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            var cart = await GetOrCreateCartAsync(userId);
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
            if(existingItem is not null)
            {
                existingItem.Quantity += dto.Quantity;
            } else
            {
                cart.Items.Add(new CartItem
                {
                   ProductId=dto.ProductId,
                   Quantity=dto.Quantity,
                   Product=existingProduct,
                   CartId=cart.Id,
                   Cart=cart
                });
            }

            await context.SaveChangesAsync();
             return new CartDto()
            {
                CartId=cart.Id,
                Items = cart.Items.Select(i=> new CartItemDto()
                {
                    ProductId=i.ProductId,
                    Quantity=i.Quantity,
                    ProductName=i.Product.Name,
                    Price=i.Product.Price

                }).ToList()
            };
        }

        private async Task<Cart> GetOrCreateCartAsync(int userId)
        {
            var cart = await context.Carts.Include(c=>c.Items).ThenInclude(i=>i.Product).FirstOrDefaultAsync(c=>c.UserId == userId);
            if(cart is not null)
            {
                return cart;
            } 
            cart = new Cart()
            {
                UserId=userId
            };
            context.Carts.Add(cart);
            await context.SaveChangesAsync();
            return await context.Carts.Include(c=>c.Items).ThenInclude(i=>i.Product).FirstOrDefaultAsync(c=>c.UserId==userId);

        }

        public async Task<CartDto?> UpdateCartItemAsync(int userId, int productId, AddToCartDto dto)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if(item is null) return null;
            item.Quantity = dto.Quantity;
            await context.SaveChangesAsync();
             return new CartDto()
            {
                CartId=cart.Id,
                Items = cart.Items.Select(i=> new CartItemDto()
                {
                    ProductId=i.ProductId,
                    Quantity=i.Quantity,
                    ProductName=i.Product.Name,
                    Price=i.Product.Price

                }).ToList()
            };
        }
        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if(item is null)
            {
                return false;
            }

            context.CartItems.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }
    }
}