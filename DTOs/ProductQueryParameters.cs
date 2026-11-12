using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs
{
    public class ProductQueryParameters
    {
        public int Page {get; set;} = 1;
        public int PageSize {get; set;} = 10;
        public string? Search {get; set;}
        public string? SortBy {get; set;}
        public decimal? MinPrice {get; set;}
        public decimal? MaxPrice {get; set;}
        public bool Descending {get; set;} = false;
    }
}