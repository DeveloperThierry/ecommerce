using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;

namespace app.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}