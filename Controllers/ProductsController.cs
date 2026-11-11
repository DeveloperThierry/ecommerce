using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
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
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var products = await context.Products.ToListAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if(product is null) return BadRequest();
            product.Id = new Random().Next(1000, 9999);
            product.CreatedAt = DateTime.UtcNow;
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new Product{Id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if(product is null) return BadRequest();
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(existingProduct is null) return NotFound();
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            await context.SaveChangesAsync();
            return Ok(existingProduct);
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