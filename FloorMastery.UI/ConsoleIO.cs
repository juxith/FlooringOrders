using FloorMastery.BLL;
using FloorMasteryModels;
using FloorMasteryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(Order order)
        {
            Console.Clear();

            Console.WriteLine("Order");
            Console.WriteLine("=======================================");
            Console.WriteLine($"{order.OrderNumber} | {FormatDateToString(order.OrderDate)}");
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"{order.State}");
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost:c}");
            Console.WriteLine($"Labor: {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.Tax:c}");
            Console.WriteLine($"Total: {order.Total:c}");
        }

        internal static void DisplayListOfProducts(List<Product> listProducts)
        {
            string line = "{0, -15}{1,-25:c}{2, -25:c}";
            Console.WriteLine();
            Console.WriteLine(line, "ProductType", "CostPerSquareFoot", "LaborCostPerSquareFoot");
            Console.WriteLine("================================================================================");
            foreach (var product in listProducts)
            {
                Console.WriteLine(line, product.ProductType, product.CostPerSquareFoot, product.LaborCostPerSquareFoot);
                Console.WriteLine();
            }
        }

        internal static string AskEditCustomerName(Order order, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().ToLower();

                
                if (string.IsNullOrEmpty(input))
                {
                    input = order.CustomerName;
                    return input;
                }
                if (input[0] == ' ')
                {
                    Console.WriteLine("Changed name cannot start wtih a space");
                }
                else
                {
                    return input;
                }
            }
        }

        internal static int GetRequiredIntFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int orderNumber))
                {
                    Console.WriteLine("You must enter valid number");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return orderNumber;
                }
            }
        }

        internal static string AskEditProductType(Order order, string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(input))
            {
                input = order.ProductType;
            }

            return input;
        }

        internal static decimal AskEditArea(Order order, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (String.IsNullOrEmpty(input))
                {
                    return order.Area;
                }
                else if (decimal.TryParse(input, out decimal output))
                {
                    if (output < 100 || output < 0)
                    {
                        Console.WriteLine("Area must be a positive value greater than 100");
                    }
                    else
                    {
                        return output;
                    }
                }
                else
                {
                    Console.WriteLine("You must enter valid decimal value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                };
            }
        }

        internal static string AskEditStateAbbrev(Order order, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (String.IsNullOrEmpty(input))
                {
                    return order.State;
                }
                else if (input.Length == 2)
                {
                    return input.ToUpper();

                }
                else
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.WriteLine("You must enter a valid state abbreviation.");
                    Console.ReadKey();
                };
            }
        }

        internal static DateTime AskEditDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out DateTime dateTime))
                {
                    Console.WriteLine("You must enter valid date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return dateTime;
                }
            }
        }

        public static void DisplayListOrderDetails(List<Order> listOfOrders)
        {
            Console.Clear();

            Console.WriteLine("Order");
            Console.WriteLine("=======================================");
            foreach (var order in listOfOrders)
            {
                Console.WriteLine($"{order.OrderNumber} | {FormatDateToString(order.OrderDate)}");
                Console.WriteLine($"{order.CustomerName}");
                Console.WriteLine($"{order.State}");
                Console.WriteLine($"Product: {order.ProductType}");
                Console.WriteLine($"Materials: {order.MaterialCost:c}");
                Console.WriteLine($"Labor: {order.LaborCost:c}");
                Console.WriteLine($"Tax: {order.Tax:c}");
                Console.WriteLine($"Total: {order.Total:c}");

                Console.WriteLine();
            }
        }

        public static string FormatDateToString(DateTime date)
        {
            return date.ToString("MMddyyyy");
        }

        internal static DateTime GetRequiredDateFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out DateTime dateTime))
                {
                    Console.WriteLine("You must enter valid date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return dateTime;
                }
            }
        }

        internal static DateTime GetRequiredAddDateFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!DateTime.TryParse(input, out DateTime dateTime))
                {
                    Console.WriteLine("You must enter valid date");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (dateTime <= DateTime.Today)
                    {
                        Console.WriteLine("Date must be in the future");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return dateTime;
                }
            }
        }


        internal static decimal GetRequiredAreaFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out decimal output))
                {
                    Console.WriteLine("You must enter valid decimal value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (output < 100 || output < 0)
                    {
                        Console.WriteLine("Area must be positive value and greater than 100.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return output;
                }
            }
        }

        internal static string GetRequiredStateFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if ((input == String.Empty) || (input.Length > 2 || input.Length < 2))
                {
                    Console.WriteLine("You must enter a valid state abbreviation.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input.ToUpper();
                }
            }
        }

        internal static string GetYesNoAnswerFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (Y/N)? ");
                string input = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter Y/N.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter Y/N.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }
        }

        internal static string RequiredStringFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || (input[0] == ' '))
                {
                    Console.WriteLine("You must enter text and it can not start with a space");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        internal static string GetRequiredProductType(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }
    }
}
