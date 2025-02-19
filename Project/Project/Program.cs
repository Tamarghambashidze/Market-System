using Project.Extensions;
using Project.Models;
using Project.Services;
using System.Text.Json;

#region Json
string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Project/users";
JsonSerializerOptions options = new JsonSerializerOptions()
{
	WriteIndented = true,
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string adminPath = Path.Combine(path, "admin.json");
string desirializedAdmin = File.ReadAllText(adminPath);
Admin admin = JsonSerializer.Deserialize<Admin>(desirializedAdmin, options);

string customerPath = Path.Combine(path, "customer list.json");
string desirializedCustomers = File.ReadAllText(customerPath);
List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(desirializedCustomers, options);

string productsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Project/products.json";
string desirializedProducts = File.ReadAllText(productsPath);
List<Product> products = JsonSerializer.Deserialize<List<Product>>(desirializedProducts, options);
#endregion
do
{
	Console.WriteLine("----- Market Project ------");
	Console.Write(" 1. Admin \n 2. User \n 3. Exit \n Answer: ");
	int answer = Console.ReadLine().TryCatchIntParse();
	switch (answer)
	{
		case 1:
			UserOrAdmin.AdminMethod(admin, customers, products);
			break;
		case 2:
			UserOrAdmin.CustomerMethod(customers, products);
			break;
		case 3:
			break;
		default:
			Console.WriteLine("Incorrect");
			continue;
	}
	if (answer == 3)
		break;
} while (true);
#region saving
string finalCustomers = JsonSerializer.Serialize(customers, options);
File.WriteAllText(customerPath, finalCustomers);

string finalAdmin = JsonSerializer.Serialize(admin, options);
File.WriteAllText(adminPath, finalAdmin);

string finalProducts = JsonSerializer.Serialize(products, options);
File.WriteAllText(productsPath, finalProducts);
#endregion