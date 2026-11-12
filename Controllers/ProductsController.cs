using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.DTOs;
using app.Models;
using app.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(AppDbContext context, IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await productService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productService.GetProductByIdAsync(id);
            if(result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if(dto is null) return BadRequest();
            var product = await productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new Product{Id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDto dto)
        {
            if(dto is null) return BadRequest();         
            var result = productService.UpdateProductAsync(id, dto);
            if(result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            if(result is false) return NotFound();
            return NoContent();
        }

    }
}