using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.DTOs
{
    public class OrderDto
    {
        public int Id {get; set;}
        public DateTime CreatedAt {get; set;}
        public OrderStatus Status {get; set;}
        public decimal TotalAmount {get; set;}
        public List<OrderItemDto> orderItems {get; set;} = new();
    }
}