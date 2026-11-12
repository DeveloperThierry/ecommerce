using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs
{
    public class CartItemDto
    {
        [Range(0, int.MaxValue)]
        public int ProductId {get; set;}
        public string  ProductName {get; set;} = string.Empty;
        [Range(0.1, 1000000000)]
        public decimal Price {get; set;}
        [Range(0, int.MaxValue)]
        public int Quantity {get; set;}
        public decimal Total => Price * Quantity;
    }
}