using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs
{
    public class CartDto
    {
        public int CartId {get; set;}
        public List<CartItemDto> Items {get; set;} = new();
        public decimal Total => Items.Sum(i => i.Total);
    }
}