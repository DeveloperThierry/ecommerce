using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.DTOs;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
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
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var products = await context.Products.ToListAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product is null) return NotFound();
            var result = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if(dto is null) return BadRequest();
            var product = new Product()
            {
                Id = new Random().Next(1000, 9999),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = DateTime.UtcNow
            };
            // product.Id = new Random().Next(1000, 9999);
            // product.CreatedAt = DateTime.UtcNow;
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new Product{Id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDto dto)
        {
            if(dto is null) return BadRequest();
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(existingProduct is null) return NotFound();
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
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product is null) return NotFound();
            context.Products.Remove(product);
            return NoContent();
        }

    }
}