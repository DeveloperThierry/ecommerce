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
    public class ProductService(AppDbContext context, ILogger<ProductService> logger):IProductService
    {
        public async Task<(IEnumerable<ProductDto> Data, int TotalCount)> GetAllProductsAsync(ProductQueryParameters query)
        {
            var productsQuery = context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(query.Search));
            }
             if (query.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= query.MinPrice.Value);
            }
            if (query.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= query.MaxPrice.Value);
            }
            var totalCount = await productsQuery.CountAsync();

            productsQuery = query.SortBy?.ToLower() switch
            {
                "price" => query.Descending ? productsQuery.OrderByDescending(p => p.Price) : productsQuery.OrderBy(p => p.Price),

                "name" => query.Descending ? productsQuery.OrderByDescending(p => p.Name) : productsQuery.OrderBy(p => p.Name),

                _ => productsQuery.OrderBy(p => p.Id)
            };
            var products = await productsQuery.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
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
            logger.LogInformation("Retrieving products with pagination");
            return (result, totalCount);
        }
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            logger.LogInformation("Retrieving product with ID: {ProductId}", id);
            var products = await context.Products.ToListAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product is null) {
                logger.LogInformation("Product fetch failed. Product with ID {ProductId} was not found", id);
                return null;}
            var result = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
            logger.LogInformation("Product fetched suucessfully with ID: {ProductID}", id);
            return result;
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            logger.LogInformation("Creating product with name : {ProductName}", dto.Name);
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
            logger.LogInformation("Product created suucessfully with ID: {ProductID}", product.Id);
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
            if(existingProduct is null) {
                logger.LogInformation("Product update failed. Product with ID {ProductId} was not found", id);
                return null;
            }
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
            logger.LogInformation("Product updated successefully. Product with ID {ProductId} was updated", id);
            return result;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product is null) {
                logger.LogInformation("Product delete failed. Product with ID {ProductID} was not found", id);
                return false;}
            logger.LogInformation("Product delete successful. Product with ID {ProductID} was deleted", id);
            context.Products.Remove(product);
            return true;
        }
    }
}