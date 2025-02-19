using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Extensions;
using Project.Models;

namespace Project.Services
{
    internal class UserOrAdmin
    {
        public static void AdminMethod(Admin admin, List<Customer> customers, List<Product> products)
        {
            do
            {
                Console.Write("Enter admin password: ");
                string password = Console.ReadLine();
                if (password == admin.Password)
                {
                    Console.WriteLine("Correct Password");
                    AdminMethods.Admin(admin, customers, products);
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect");
                }
                Console.Write("Do you want to continue? \n 1. Yes \n 2. No");
                int answer = Console.ReadLine().TryCatchIntParse();
                if (answer == 2)
                    break;
                else if(answer != 2 && answer != 1)
                    Console.WriteLine("Incorrect input");
            } while (true);
        }
        public static void CustomerMethod(List<Customer> list, List<Product> products)
        {
            do
            {
                Console.Write("1. Registrate\n2. Log in \n Answer: ");
                int logInOrRegistrate = Console.ReadLine().TryCatchIntParse();
                Customer user = default;
                switch (logInOrRegistrate)
                {
                    case 1:
                        user = RegistrationAndLogIn.Registration(list);
                        break;
                    case 2:
                        user = RegistrationAndLogIn.LogIn(list);
                        break;
                    default:
                        Console.WriteLine("incorrect input");
                        continue;
                }
                CustomerMethods.Customer(user, products, list);
                break;
            } while (true);
        }
    }
}
