using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs
{
    public class OrderItemDto
    {
        public int ProductId {get; set;}
        public string ProductName {get; set;} = string.Empty;
        public decimal UnitPrice {get; set;}
        public int Quantity {get; set;}
        public decimal Total => Quantity * UnitPrice;
    }
}