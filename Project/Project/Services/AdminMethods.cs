using Project.Extensions;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    internal class AdminMethods
    {
        public static void Admin(Admin admin, List<Customer> customers, List<Product> products)
        {
            AdminCRUD adminCRUD = new AdminCRUD() { Admin = admin, Products = products, Users = customers};
            do
            {
                Console.Write("1. See your information\n2. Manage users\n3. Manage products\n4. Exit" +
                    "\n Answer: ");
                int answer = Console.ReadLine().TryCatchIntParse();
                if(answer == 1)
                {
                    admin.Print();
                    Console.Write("Was this you?\n 1. yes\n 2. no\n Answer: ");
                    int answer2;
                    do
                    {
                        answer2 = Console.ReadLine().TryCatchIntParse();
                        if (answer2 == 2)
                            adminCRUD.ChangePassword();
                    } while (answer2 != 1 && answer2 != 2);
                    admin.LastLogIn = DateTime.Now;
                }
                else if(answer == 2)
                {
                    Console.Write("1. See all users\n2. Remove user\n3. Search user\n Answer: ");
                    int answer2 = Console.ReadLine().TryCatchIntParse();
                    switch(answer2)
                    {
                        case 1: 
                            customers.Print();
                            break;
                        case 2:
                            adminCRUD.RemoveUser();
                            break;
                        case 3:
                            customers.Search().Print();
                            break;
                        default:
                            Console.WriteLine("incorrect input");
                            break;
                    }
                }
                else if(answer == 3)
                {
                    Console.Write("1. See all products\n2. Restock\n3. Remove\n4. Add\n" +
                        "5. Change product price\n Answer: ");
                    int answer2 = Console.ReadLine().TryCatchIntParse();
                    switch(answer2)
                    {
                        case 1:
                            products.Print(); 
                            break;
                        case 2:
                            adminCRUD.Restock();
                            break;
                        case 3:
                            adminCRUD.RemoveProduct();
                            break;
                        case 4:
                            adminCRUD.AddProduct();
                            break;
                        case 5:
                            adminCRUD.ChangePrice();
                            break;
                    }
                }
                else if(answer == 4)
                    break;
                else
                    Console.WriteLine("incorrect input");
            } while (true);
        }
    }
}
