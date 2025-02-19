using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Project.Abstracts;
using Project.Exceptions;
using Project.Extensions;
using Project.Models;

namespace Project.Services
{
    internal class AdminCRUD : IAdmin
    {
        public Admin Admin { get; set; }
        public List<Customer> Users { get; set; }
        public List<Product> Products { get; set; }

        public void ChangePassword()
        {
            do
            {
                Console.Write("Enter old password: ");
                string oldPassword = Console.ReadLine();
                if (oldPassword != Admin.Password)
                    Console.WriteLine("Incorrect password");
                else
                {
                    Console.Write("Enter new password: ");
                    try
                    {
                        Admin.Password = Console.ReadLine();
                        if (Admin.Password.Length < 4)
                            throw new AdminException("Password must be longer than 4");
                        else if (Admin.Password == oldPassword)
                            throw new AdminException("Same password");
                        else
                            break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            } while (true);
        }
        public void RemoveUser()
        {
            Customer customer = Users.Search();
            if(customer != null)
            {
                Users.Remove(customer);
                Console.WriteLine("User removed successfully");
            }
            else
                Console.WriteLine("User not found");
        }
        public void Restock()
        {
            Product product = Products.Search();
            if(product != null)
            {
                Console.Write("Enter amount: ");
                int amount = Console.ReadLine().TryCatchIntParse();
                if (amount > 0)
                {
                    product.QNT += amount;
                    Console.WriteLine("Product restocked successfully");
                }
                else
                    Console.WriteLine("incorrect input");
            }
            else
                Console.WriteLine("Product not found");
        }
        public void RemoveProduct()
        {
            Product product = Products.Search();
            if(product != null)
            {
                Products.Remove(product);
                Console.WriteLine("Product removed successfully");
            }
            else
                Console.WriteLine("product not found");
        }
        public void AddProduct()
        {
            Product product = new Product();
            do
            {
                Console.Write("Enter product name: ");
                product.ProductName = Console.ReadLine();
                Console.Write("Choose product Type\n 1. Snack\n 2. Drink\n 3. Candy\n  Answer: ");
                int answer;
                do
                {
                    answer = Console.ReadLine().TryCatchIntParse();
                    switch (answer)
                    {
                        case 1:
                            product.ProductType = "Snack";
                            break;
                        case 2:
                            product.ProductType = "Drink";
                            break;
                        case 3:
                            product.ProductType = "Candy";
                            break;
                        default:
                            Console.WriteLine("Incorrect input");
                            break;
                    }
                } while (answer != 1 && answer != 2 && answer != 3);
                Console.Write("Enter product manufacturer: ");
                product.Manufacturer = Console.ReadLine();
                Console.Write("Enter product price: ");
                double price = 0;
                do
                {
                    try
                    {
                        price = double.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                } while (!(price > 0));
                product.Price = price;
                Console.Write("Enter product quantity: ");
                product.QNT = Console.ReadLine().TryCatchIntParse();
                Console.Write("Enter product description: ");
                product.Description = Console.ReadLine();
                try
                {
                    bool boolean = product.ProductName.Length < 3 || product.QNT == null ||
                        product.Description.Length < 3 || product.Manufacturer.Length < 3;
                    Product item = Products.FirstOrDefault(p => p.ProductName == product.ProductName);
                    if (boolean)
                        throw new ProductException("Product name, manufacturer or description is unfilled");
                    else if (item != null)
                        throw new ProductException("This product already exists");
                    else
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    product = null;
                }
            } while (product != null);
            Console.WriteLine("product added successfully");
        }
        public void ChangePrice()
        {
            Product product = Products.Search();
            if (product != null)
            {
                Console.Write("Enter new price: ");
                double price = 0;
                do
                {
                    try
                    {
                        price = double.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                } while (!(price > 0));
                product.Price = price;
                Console.WriteLine("Price changed successfully");
            }
            else
                Console.WriteLine("Product not found");
        }
    }
}
