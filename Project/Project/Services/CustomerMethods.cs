using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Project.Extensions;
using Project.Models;

namespace Project.Services
{
    internal class CustomerMethods
    {
        public static void Customer(Customer customer, List<Product> products, List<Customer> customers)
        {
            CustomerCRUD customerCRUD = new CustomerCRUD();
            customerCRUD.Customer = customer;
            Console.WriteLine($"\n Hello {customer.FirstName}");
            do
            {
                Console.Write("1.Products\n2.Your info \n3.Exit\n Answer: ");
                int answer = Console.ReadLine().TryCatchIntParse();
                if (answer == 1)
                {
                    Console.Write("1. See all products \n2. See grouped products \n3. Order by price" +
                        "\n Answer: ");
                    int answer2 = Console.ReadLine().TryCatchIntParse();
                    if (answer2 == 1)
                    {
                        products.Print();
                        ViewProduct(products, customerCRUD);
                    }
                    else if (answer2 == 2)
                    {
                        Console.Write("1. group by manufacuter\n2. group by type \n Answer: ");
                        int answer3 = Console.ReadLine().TryCatchIntParse();
                        if (answer3 == 1)
                        {
                            var groupedProducts = products.GroupBy(p => p.Manufacturer);
                            foreach (var item in groupedProducts)
                            {
                                Console.WriteLine($"\nManufacturer: {item.Key}");
                                item.ToList().Print();
                            }
                            ViewProduct(products, customerCRUD);
                        }
                        else if (answer3 == 2)
                        {
                            var groupedProducts = products.GroupBy(p => p.ProductType);
                            Console.Write("Product types\n1. Drinks\n2. Candy\n3. Snacks\n Answer: ");
                            int answer4 = Console.ReadLine().TryCatchIntParse();
                            if (answer4 == 1 || answer4 == 2 || answer4 == 3)
                            {
                                if (answer4 == 1)
                                {
                                    foreach (var item in groupedProducts)
                                    {
                                        if (item.Key == "Drink")
                                        {
                                            Console.WriteLine($"\nType: {item.Key}");
                                            item.ToList().Print();
                                        }
                                    }
                                }
                                else if (answer4 == 2)
                                {
                                    foreach (var item in groupedProducts)
                                    {
                                        if (item.Key == "Candy")
                                        {
                                            Console.WriteLine($"\nType: {item.Key}");
                                            item.ToList().Print();
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var item in groupedProducts)
                                    {
                                        if (item.Key == "Snack")
                                        {
                                            Console.WriteLine($"\nType: {item.Key}");
                                            item.ToList().Print();
                                        }
                                    }
                                }
                                ViewProduct(products, customerCRUD);
                            }
                            else
                                Console.WriteLine("Incorrect input");
                        }
                        else
                            Console.WriteLine("Incorrect");
                    }
                    else if (answer2 == 3)
                    {
                        Console.Write("1.Escening\n2.Descening\n Answer: ");
                        int answer4 = Console.ReadLine().TryCatchIntParse();
                        switch (answer4)
                        {
                            case 1:
                                products.OrderBy(p => p.Price).ToList().Print();
                                break;
                            case 2:
                                products.OrderByDescending(p => p.Price).ToList().Print();
                                break;
                        }
                        ViewProduct(products, customerCRUD);
                    }
                    else
                        Console.WriteLine("Incorrect");
                }
                else if (answer == 2)
                {
                    Console.Write("1. See all your information\n2. View basket\n3. View favourites" +
                        "\n4. Change your password\n5. Change your username \n6. Recharge balance" +
                        "\n7. Remove your account\n Answer: ");
                    int answer3 = Console.ReadLine().TryCatchIntParse();
                    switch (answer3)
                    {
                        case 1:
                            Console.WriteLine(customer);
                            break;
                        case 2:
                            ViewBasket(products, customerCRUD);
                            break;
                        case 3:
                            ViewFavourites(products, customerCRUD);
                            break;
                        case 4:
                            customerCRUD.ChangePassword();
                            break;
                        case 5:
                            customerCRUD.ChangeUsername(customers);
                            break;
                        case 6:
                            Console.Write("Amount: ");
                            int amount = Console.ReadLine().TryCatchIntParse();
                            customerCRUD.RechargeBalance(amount);
                            break;
                        case 7:
                            customers.Remove(customer);
                            Console.WriteLine("You Have Removed your account successfully");
                            break;
                        default:
                            Console.WriteLine("Incorrect");
                            continue;
                    }
                }
                else if (answer == 3)
                    break;
                else
                {
                    Console.WriteLine("Incorrect input");
                    continue;
                }
            } while (true);
        }
        static void ViewProduct(List<Product> list, CustomerCRUD crud)
        {
            Console.Write("Do you want to see product?(yes/no) - ");
            string answer1 = Console.ReadLine();
            if (answer1 == "yes")
            {
                Product product = default;
                do
                {
                    product = list.Search();
                    if (product != null)
                    {
                        Console.WriteLine($"{product} \n  {product.Description}");
                        break;
                    }
                    else
                        Console.WriteLine("Product not found");
                } while (true);
                Console.Write("\n 1. Add to basket \n 2. Add to favourites\n 3. Exit \n Answer: ");
                int answer2 = Console.ReadLine().TryCatchIntParse();
                switch (answer2)
                {
                    case 1:
                        crud.Add(product, crud.Customer.Basket, list);
                        break;
                    case 2:
                        crud.Add(product, crud.Customer.Favourites, list);
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("incorrect input");
                        break;
                }
            }
        }
        static void ViewBasket(List<Product> list, CustomerCRUD crud)
        {
            Console.WriteLine("Basket");
            crud.Customer.Basket.Print();
            do
            {
                Console.Write(" 1. Remove product \n 2. Buy product \n 3. Exit \n  Answer: ");
                int answer = Console.ReadLine().TryCatchIntParse();
                if (answer == 1)
                {
                    Product p = list.Search();
                    if (p != null)
                        crud.Remove(p, crud.Customer.Basket);
                }
                else if(answer == 2)
                {
                    Console.Write("1. Buy one \n2. Buy more than 1\n3. Buy all\n4. Exit \n Answer: ");
                    int answer2 = Console.ReadLine().TryCatchIntParse();
                    if (answer2 == 1 || answer2 == 2)
                    {
                        Product product = crud.Customer.Basket.Search();
                        if(product != null)
                        {
                            if (answer2 == 1)
                                crud.BuyProduct(product, list);
                            else if (answer2 == 2)
                            {
                                int qnt = Console.ReadLine().TryCatchIntParse();
                                crud.BuyProduct(product, list, qnt);
                            }
                        }
                    }
                    else if (answer2 == 3)
                        crud.BuyAll(list);
                    else if (answer2 == 4)
                        break;
                    else
                    {
                        Console.WriteLine("Incorrect input");
                        continue;
                    }
                }
                else if (answer == 3)
                    break;
                else
                    Console.WriteLine("Incorrect input");
            } while (true);
        }
        static void ViewFavourites(List<Product> list, CustomerCRUD crud)
        {
            Console.WriteLine("Favourites");
            crud.Customer.Favourites.Print();
            do
            {
                Console.Write(" 1. Remove Product \n 2. Add to basket \n 3. Exit \n Answer: ");
                int answer = Console.ReadLine().TryCatchIntParse();
                if (answer == 1 || answer == 2)
                {
                    Product product = crud.Customer.Favourites.Search();
                    if (product != null)
                    {
                        crud.Remove(product, crud.Customer.Favourites);
                        if (answer == 2)
                        {
                            crud.Add(product, crud.Customer.Basket, crud.Customer.Favourites);
                            crud.Customer.Favourites.Remove(product);
                        }
                    }
                    else
                        Console.WriteLine("Product not found");
                }
                else if (answer == 3)
                    break;
                else
                    Console.WriteLine("Incorrect product");
            } while (true);
        }
    }
}
