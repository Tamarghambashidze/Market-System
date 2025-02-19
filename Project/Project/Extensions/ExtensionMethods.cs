using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Extensions
{
    internal static class ExtensionMethods
    {
        public static int TryCatchIntParse(this string text)
        {
            int result = default;
            try
            {
                result = int.Parse(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static void Print<T>(this T text)
        {
            Console.WriteLine(text);
        }
        public static void Print<T>(this List<T> text)
        {
            foreach (T item in text)
            {
                Console.WriteLine(item);
            }
        }
        public static Customer Search(this List<Customer> list)
        {
            Console.Write("Enter customer's username: ");
            string username = Console.ReadLine();
            return list.FirstOrDefault(i => i.UserName == username);
        }
        public static Product Search(this List<Product> list)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            return list.FirstOrDefault(i => i.ProductName == name);
        }
    }
}
