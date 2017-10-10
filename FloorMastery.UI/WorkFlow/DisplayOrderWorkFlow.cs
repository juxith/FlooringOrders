using FloorMastery.BLL;
using FloorMasteryModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery.WorkFlow
{
    public class DisplayOrderWorkFlow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("--------------------------------");

            Console.Write("Enter order date: ");

            DateTime orderDate = ConsoleIO.GetRequiredDateFromUser("Enter order date. Ex. MM/DD/YYYYY : ");

            DisplayOrderResponse response = orderManager.DisplayOrder(orderDate);

            if (response.Success)
            {
                ConsoleIO.DisplayListOrderDetails(response.ListOfOrders);

            }
            else
            {
                Console.WriteLine("An error occurerd: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
