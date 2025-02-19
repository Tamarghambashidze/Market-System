using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Abstracts;
using Project.Exceptions;
using Project.Extensions;
using Project.Models;

namespace Project.Services
{
    internal class CustomerCRUD : ICostumer
    {
        public Customer Customer { get; set; }

        void CheckQuantity(Product product, List<Product> list)
        {
            if (product.QNT == 0)
                list.Remove(product);
        }
        public void RechargeBalance(double amount)
        {
            Customer.Balance += amount;
            Console.WriteLine($"Balance: {Customer.Balance}$");
        }
        public void Add(Product product, List<Product> addList, List<Product> RemoveList)
        {
            Product removeProduct = RemoveList.FirstOrDefault(p => p.ProductName == product.ProductName);
            Product addProduct = addList.FirstOrDefault(p => p.ProductName == product.ProductName);
            if(removeProduct == null || removeProduct.QNT == 0)
                Console.WriteLine("Product is not available");
            else
            {
                if (addProduct == null)
                {
                    addList.Add(new Product()
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Description = product.Description,
                        QNT = 1,
                        Manufacturer = product.Manufacturer,
                        ProductType = product.ProductType
                    });
                }
                else
                    addProduct.QNT++;
                Console.WriteLine("Product added successfully");
            }
        }

        public void Remove(Product product, List<Product> products)
        {
            Product item = products.FirstOrDefault(product);
            if (item != null)
            {
                if (product.QNT > 1)
                {
                    Console.Write("1. All\n2. 1 product\n Answer: ");
                    int answer = Console.ReadLine().TryCatchIntParse();
                    switch (answer)
                    {
                        case 1:
                            products.Remove(product);
                            break;
                        case 2:
                            product.QNT--;
                            break;
                        default:
                            Console.WriteLine("incorrect input");
                            break;
                    }
                }
                else
                    products.Remove(product);
                Console.WriteLine("Removed Successfully");
            }
            else
                Console.WriteLine("Product not found");
        }
        bool CheckUser()
        {
            Console.Write("Enter your current password: ");
            string password = Console.ReadLine();
            return password == Customer.Password;
        }
        public void ChangePassword()
        {
            if (CheckUser())
            {
                do
                {
                    Console.Write("New Password: ");
                    try
                    {
                        Customer.Password = Console.ReadLine();
                        if (Customer.Password.Length < 8)
                            throw new RegistrationException("Password must be longer than 8");
                        else
                        {
                            Console.WriteLine("Successfully changed password");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (true);
            }
            else
                Console.WriteLine("incorrect password");
        }
        public void ChangeUsername(List<Customer> list)
        {
            if (CheckUser())
            {
                do
                {
                    Console.Write("New username: ");
                    try
                    {
                        string username = Console.ReadLine();
                        if (Customer.UserName.Length < 3)
                            throw new RegistrationException("Username must be longer than 3");
                        else if (list.FirstOrDefault(i => i.UserName == username) != null)
                            throw new RegistrationException("this username already exists");
                        else
                        {
                            Customer.UserName = username;
                            Console.WriteLine("Successfully changed username");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (true);
            }
            else
                Console.WriteLine("incorrect password");
        }
        public void BuyProduct(Product product, List<Product> allProducts)
        {
            Product p = Customer.Basket.FirstOrDefault(p => p == product);
            if (p != null && p.Price <= Customer.Balance)
            {
                Product item = allProducts.FirstOrDefault(pr => pr.ProductName == product.ProductName);
                Customer.Balance -= p.Price;
                p.QNT--;
                item.QNT--;
                CheckQuantity(product, Customer.Basket);
                Console.WriteLine("Bought successfully");
            }
            else if (p != null)
                Console.WriteLine("Product not found");
            else if (p.Price > Customer.Balance)
                Console.WriteLine("Not enough money on your Balance");
        }
        public void BuyProduct(Product product, List<Product> allProducts, int quantity)
        {
            Product p = Customer.Basket.FirstOrDefault(p => p == product);
            if (p != null && p.Price * quantity <= Customer.Balance && p.QNT >= quantity)
            {
                for (int i = 0; i < quantity; i++)
                {
                    Customer.Balance -= p.Price;
                    p.QNT--;
                    allProducts.FirstOrDefault(pr => pr.ProductName == p.ProductName).QNT--;
                }
                CheckQuantity(product, Customer.Basket);
            }
            else if (p != null)
                Console.WriteLine("Product not found");
            else if (p.Price * quantity > Customer.Balance)
                Console.WriteLine("Not enough money on your Balance");
            else if (quantity > p.QNT)
                Console.WriteLine($"There are only {p.QNT} products in your basket");
        }
        public void BuyAll(List<Product> allProducts)
        {
            double amount = 0;
            foreach (Product p in Customer.Basket)
            {
                for (int i = 0; i < p.QNT; i++)
                {
                    amount += p.Price;
                }
            }
            if (amount <= Customer.Balance)
            {
                Console.WriteLine("Bought successfully");
                Customer.Balance -= amount;
                Customer.Basket.ForEach(p =>
                {
                    allProducts.FirstOrDefault(pr => pr.ProductName == p.ProductName).QNT -= p.QNT;
                });
                Customer.Basket.Clear();
            }
            else
                Console.WriteLine("Not enough money on your balance");
        }
    }
}
