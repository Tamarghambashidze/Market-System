using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    internal class Customer : User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public List<Product> Basket { get; set; }
        public List<Product> Favourites { get; set; }
        public override string ToString()
        {
            return base.ToString() + $" - {UserName} - {Email} - Balance: {Balance}$";
        }
    }
}
