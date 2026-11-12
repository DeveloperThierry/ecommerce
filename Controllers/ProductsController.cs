using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.DTOs;
using app.Models;
using app.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters query)
        {
            var (result, totalCount) = await productService.GetAllProductsAsync(query);
            var response = new
            {
                data=result,
                totalCount,
                query.Page,
                query.PageSize
            };
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productService.GetProductByIdAsync(id);
            if(result is null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if(dto is null) return BadRequest();
            var product = await productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new Product{Id = product.Id}, product);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDto dto)
        {
            if(dto is null) return BadRequest();         
            var result = productService.UpdateProductAsync(id, dto);
            if(result is null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            if(result is false) return NotFound();
            return NoContent();
        }

        // [HttpGet("test-error")]
        // public IActionResult TestError()
        // {
        //     throw new Exception("This is a test exception from products controller");
        // }

    }
}