using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Exceptions;
using Project.Models;

namespace Project.Services
{
    internal class RegistrationAndLogIn
    {
        public static Customer Registration(List<Customer> list)
        {
            bool boolean = true;
            Customer user = new Customer();
            while (boolean)
            {
                Console.WriteLine("Registration");
                Console.Write("First Name: ");
                user.FirstName = Console.ReadLine();
                Console.Write("Last Name: ");
                user.LastName = Console.ReadLine();
                Console.Write("Username: ");
                user.UserName = Console.ReadLine();
                Console.Write("Email: ");
                user.Email = Console.ReadLine();
                Console.Write("Password: ");
                user.Password = Console.ReadLine();
                try
                {
                    if (user.FirstName.Length < 4 || user.LastName.Length < 4 || user.UserName.Length < 4)
                        throw new RegistrationException("Name must be longer than 3");
                    else if (!user.Email.Contains('@') || !user.Email.Contains('.'))
                        throw new RegistrationException("Email must contain '@' and '.'");
                    else if (user.Password.Length < 8)
                        throw new RegistrationException("Password must be longer than 8");
                    else
                        boolean = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    boolean = true;
                    continue;
                }
                Customer customer = list.FirstOrDefault(item => item.Email == user.Email || item.UserName == user.UserName);
                if (customer == null)
                {
                    user.Basket = new List<Product>();
                    user.Favourites = new List<Product>();
                    user.Balance = 0;
                    Console.WriteLine("Successfull registration");
                    list.Add(user);
                    boolean = false;
                }
                else
                {
                    Console.WriteLine("This account already exists");
                    boolean = true;
                }
            }
            return user;
        }

        public static Customer LogIn(List<Customer> list)
        {
            Customer customer = null;
            do
            {
                Console.WriteLine("Log in");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                customer = list.FirstOrDefault(c => c.UserName == username && c.Password == password);
                if (customer == null)
                    Console.WriteLine("user not found");
                else
                {
                    Console.WriteLine("Successfull log in");
                    break;
                }
            } while (true);
            return customer;
        }
    }
}
