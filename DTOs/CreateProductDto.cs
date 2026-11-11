using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name {get; set;} = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Description {get; set;} = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.1, 1000000000)]
        public decimal Price {get; set;}
        [Range(0,int.MaxValue)]
        public int Stock {get; set;}
    }
}