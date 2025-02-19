using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstracts
{
    internal interface IAdmin
    {
        public void ChangePassword();
        public void RemoveUser();
        public void Restock();
        public void RemoveProduct();
        public void AddProduct();
        public void ChangePrice();
    }
}
