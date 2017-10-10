using FloorMastery.BLL;
using FloorMasteryModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery.WorkFlow
{
    public class EditOrderWorkFlow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Edit Orders");
            Console.WriteLine("--------------------------------");

            DateTime orderDate = ConsoleIO.AskEditDateTime("Enter order date: ");

            DisplayOrderResponse displayResponse = orderManager.DisplayOrder(orderDate);

            if (!displayResponse.Success)
            {
                Console.WriteLine("An error occurerd: ");
                Console.WriteLine(displayResponse.Message);
            }
            else
            {
                ConsoleIO.DisplayListOrderDetails(displayResponse.ListOfOrders);
                int orderNumber = ConsoleIO.GetRequiredIntFromUser("Enter order number: ");

                EditOrderResponse editResponse = orderManager.EditOrder(orderDate, orderNumber);

                if (!editResponse.Success)
                {
                    Console.WriteLine("An error occured");
                    Console.WriteLine(editResponse.Message);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Enter changes as they appear. If you wish not to change anything just press 'Enter': ");
                    string customerName = ConsoleIO.AskEditCustomerName(editResponse.Order, $"Name: ({editResponse.Order.CustomerName})  ");
                    string stateAbbrev = ConsoleIO.AskEditStateAbbrev(editResponse.Order, $"State: ({editResponse.Order.State})  ");
                    string productType = ConsoleIO.AskEditProductType(editResponse.Order, $"Product Type: ({editResponse.Order.ProductType})  ");
                    decimal area = ConsoleIO.AskEditArea(editResponse.Order, $"Area: ({editResponse.Order.Area})  ");

                    AddOrderResponse addResponse = orderManager.AddOrder(orderDate, customerName, stateAbbrev, productType, area, orderNumber);

                    if (!addResponse.Success)
                    {
                        Console.WriteLine("An error occured");
                        Console.WriteLine(addResponse.Message);
                        //Console.WriteLine("Press any key to continue...");
                        //Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine();
                        ConsoleIO.DisplayOrderDetails(addResponse.Order);
                        Console.WriteLine();
                        if (ConsoleIO.GetYesNoAnswerFromUser("Add the followng changes") == "Y")
                        {
                            //need to update instead of save. 
                            orderManager.UpdateOrder(addResponse.Order);
                            Console.WriteLine("Order updated!");
                            //Console.WriteLine("Press any key to continue...");
                            //Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Edits Cancelled");
                            //Console.WriteLine("Press any key to continue...");
                            //Console.ReadKey();
                        }
                    }
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            /*Ask user for Orderdate and then orderNumber. 
             * find order and load that order. 
             * once order loaded, run through each property and ask if they'd like to change it. If no than re-assing to itself. if yes, assign it to new info. 
             * once all fields have been fulfilled save like add to repo.*/

        }
    }
}
