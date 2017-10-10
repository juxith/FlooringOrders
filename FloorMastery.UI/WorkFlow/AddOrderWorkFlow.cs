using FloorMastery.BLL;
using FloorMasteryModels;
using FloorMasteryModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery.WorkFlow
{
    public class AddOrderWorkFlow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Add Order");
            Console.WriteLine("--------------------------------");

            DateTime date = ConsoleIO.GetRequiredAddDateFromUser("Enter order date. Ex. MM/DD/YYYYY : ");
            string customerName = ConsoleIO.RequiredStringFromUser("CustomerName: ");
            string state = ConsoleIO.GetRequiredStateFromUser("State abbreviation: ");

            List<Product> listProducts = orderManager.GetListOfProducts();
            ConsoleIO.DisplayListOfProducts(listProducts);

            string productType = ConsoleIO.GetRequiredProductType("Enter product type: ");
            decimal area = ConsoleIO.GetRequiredAreaFromUser("Area: ");

            AddOrderResponse response = orderManager.AddOrder(date, customerName, state, productType, area, 0);

            if (!response.Success)
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(response.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine();
                ConsoleIO.DisplayOrderDetails(response.Order);
                Console.WriteLine();
                if (ConsoleIO.GetYesNoAnswerFromUser("Add the followng order") == "Y")
                {
                    orderManager.AddToOrderRepo(response.Order);
                    Console.WriteLine("Order Added!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Order Cancelled");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
