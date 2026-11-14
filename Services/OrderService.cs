using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.DTOs;
using app.Mappers;
using app.Models;
using app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class OrderService(AppDbContext context):IOrderService
    {
        public async Task<OrderDto> CheckoutAsync(int userId){
            var cart = await context.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            if(cart is null || !cart.Items.Any()) throw new InvalidOperationException("Cart is Empty");
            var order = new Order
            {
                UserId=userId,
                CreatedAt=DateTime.UtcNow,
                Status=OrderStatus.Pending,
                Items=cart.Items.Select(i => new OrderItem
                {
                    ProductId=i.ProductId,
                    Product=i.Product,
                    ProductName=i.Product.Name,
                    UnitPrice=i.Product.Price,
                    Quantity=i.Quantity
                }).ToList()
            };
            order.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);
            context.Orders.Add(order);
            context.CartItems.RemoveRange(cart.Items);
            await context.SaveChangesAsync();
            return OrderMapper.FromOrderToDto(order);

        }
        public async Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int userId){
            var orders = await context.Orders.Include(c => c.Items).Where(c => c.UserId == userId).OrderByDescending(c => c.CreatedAt).ToListAsync();
            return orders.Select(o => OrderMapper.FromOrderToDto(o)).ToList();
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(){
            var orders = await context.Orders.Include(c => c.Items).OrderByDescending(c => c.CreatedAt).ToListAsync();
            return orders.Select(o => OrderMapper.FromOrderToDto(o)).ToList();
        }
        public async Task<OrderDto> UpdateOrderStatusAsync(int orderId, OrderStatus status){
            var order = await context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId);
            if(order is null) return null;
            order.Status = status;
            await context.SaveChangesAsync();
            return OrderMapper.FromOrderToDto(order);
        }
    }
}