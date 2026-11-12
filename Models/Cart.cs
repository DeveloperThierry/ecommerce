using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class Cart
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public User User {get; set;} = null;
        public List<CartItem> Items {get; set;} = new ();
    }
}