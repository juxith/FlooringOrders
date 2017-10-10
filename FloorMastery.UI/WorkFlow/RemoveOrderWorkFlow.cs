using FloorMastery.BLL;
using FloorMasteryModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery.WorkFlow
{
    public class RemoveOrderWorkFlow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Remove Order");
            Console.WriteLine("--------------------------------");

            DateTime orderDate = ConsoleIO.AskEditDateTime("Enter order date: ");

            DisplayOrderResponse displayResponse = orderManager.DisplayOrder(orderDate);

            if (displayResponse.Success)
            {
                ConsoleIO.DisplayListOrderDetails(displayResponse.ListOfOrders);
            }
            else
            {
                Console.WriteLine("An error occurerd: ");
                Console.WriteLine(displayResponse.Message);
            }

            int orderNumber = ConsoleIO.GetRequiredIntFromUser("Enter order number: ");

            EditOrderResponse editResponse = orderManager.EditOrder(orderDate, orderNumber);

            if (!editResponse.Success)
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(editResponse.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine();
                ConsoleIO.DisplayOrderDetails(editResponse.Order);
                Console.WriteLine();
                if (ConsoleIO.GetYesNoAnswerFromUser("Are you sure you want to remove this order?") == "Y")
                {
                    //need to update instead of save. 
                    orderManager.RemoveOrder(editResponse.Order);
                    Console.WriteLine("Order removed");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Remove Cancelled");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
