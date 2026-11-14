using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;

namespace app.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task<CartDto> AddToCartAsync(int userId, AddToCartDto dto);
        Task<CartDto?> UpdateCartItemAsync(int userId, int productId, AddToCartDto dto);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
    }
}