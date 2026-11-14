using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Models;

namespace app.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto FromOrderToDto(this Order order)
        {
            return new OrderDto
            {
                Id=order.Id,
                CreatedAt=order.CreatedAt,
                Status=order.Status,
                TotalAmount=order.TotalAmount,
                orderItems = order.Items.Select(i=>new OrderItemDto
                {
                   ProductId=i.ProductId,
                    ProductName=i.ProductName,
                    UnitPrice=i.UnitPrice,
                    Quantity=i.Quantity,
                }).ToList()
            };
        }
    }
}