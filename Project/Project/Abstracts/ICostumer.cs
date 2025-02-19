using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstracts
{
    internal interface ICostumer
    {
        public void RechargeBalance(double amount);
        public void Add(Product product, List<Product> products1, List<Product> products2);
        public void Remove(Product product, List<Product> products);
        public void ChangePassword();
        public void ChangeUsername(List<Customer> customers);
        public void BuyProduct(Product product, List<Product> list);
        public void BuyProduct(Product product, List<Product> list, int quantity);
        public void BuyAll(List<Product> list);

    }
}
