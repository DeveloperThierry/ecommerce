using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs
{
    public class ProductListResponse
    {
        public IEnumerable<ProductDto> Data {get; set;} = new List<ProductDto>();
        public int TotalCount {get; set;}
    }
}