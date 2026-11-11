using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "High Performance Laptop",
                    Price = 1200.00m,
                    Stock = 10
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic Bluetooth Mouse",
                    Price = 29.99m,
                    Stock = 50
                },
                new Product
                {
                    Id = 3,
                    Name = "Mechanical Keyboard",
                    Description = "RGB Backlit Gaming Keyboard",
                    Price = 89.99m,
                    Stock = 25
                },
                new Product
                {
                    Id = 4,
                    Name = "Monitor",
                    Description = "27-inch 4K UHD IPS Display",
                    Price = 349.50m,
                    Stock = 15
                },
                new Product
                {
                    Id = 5,
                    Name = "USB-C Hub",
                    Description = "7-in-1 Multi-Port Adapter",
                    Price = 45.00m,
                    Stock = 40
                }
            };

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "High Performance Laptop",
                    Price = 1200.00m,
                    Stock = 10
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic Bluetooth Mouse",
                    Price = 29.99m,
                    Stock = 50
                },
                new Product
                {
                    Id = 3,
                    Name = "Mechanical Keyboard",
                    Description = "RGB Backlit Gaming Keyboard",
                    Price = 89.99m,
                    Stock = 25
                },
                new Product
                {
                    Id = 4,
                    Name = "Monitor",
                    Description = "27-inch 4K UHD IPS Display",
                    Price = 349.50m,
                    Stock = 15
                },
                new Product
                {
                    Id = 5,
                    Name = "USB-C Hub",
                    Description = "7-in-1 Multi-Port Adapter",
                    Price = 45.00m,
                    Stock = 40
                }
            };

            var product = products.FirstOrDefault(p => p.Id == id);
            if(product is null) return NotFound();

            return Ok(product);
        }
    }
}