using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    internal class Product
    {
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Manufacturer { get; set; }
        public int QNT { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{ProductName} - {Price}$ - Quantity: {QNT}";
        }
    }
}
