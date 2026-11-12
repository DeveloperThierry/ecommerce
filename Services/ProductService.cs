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
    public class ProductService(AppDbContext context):IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await context.Products.ToListAsync();
            var result = products.Select(
                p => new ProductDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }
            );
            return result;
        }
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var products = await context.Products.ToListAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product is null) return null;
            var result = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };

            return result;
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product()
            {
                Id = new Random().Next(1000, 9999),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = DateTime.UtcNow
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
            };
        }
        public async Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto)
        {
             var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(existingProduct is null) return null;
            existingProduct.Name = dto.Name;
            existingProduct.Description = dto.Description;
            existingProduct.Price = dto.Price;
            existingProduct.Stock = dto.Stock;
            await context.SaveChangesAsync();
            var result = new ProductDto()
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                Stock = existingProduct.Stock,
            };
            return result;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product is null) return false;
            context.Products.Remove(product);
            return true;
        }
    }
}