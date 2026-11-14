using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;

namespace app.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CheckoutAsync(int userId);
        Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int userId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}