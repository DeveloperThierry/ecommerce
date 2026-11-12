using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users {get; set;}
        public DbSet<Cart> Carts {get; set;}
        public DbSet<CartItem> CartItems {get; set;}
    }
}