using FloorMastery.BLL;
using FloorMastery.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery
{
    class Menu
    {
        public static void Start()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("SWC Corp Flooring Orders");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");
                Console.WriteLine("5. Quit");

                Console.WriteLine("\nEnter selection: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        DisplayOrderWorkFlow displayOrderWorkFlow = new DisplayOrderWorkFlow();
                        displayOrderWorkFlow.Execute();
                        break;
                    case "2":
                        AddOrderWorkFlow addOrderWorkFlow = new AddOrderWorkFlow();
                        addOrderWorkFlow.Execute();
                        break;
                    case "3":
                        EditOrderWorkFlow editOrderWork = new EditOrderWorkFlow();
                        editOrderWork.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkFlow removeOrderWorkFlow = new RemoveOrderWorkFlow();
                        removeOrderWorkFlow.Execute();
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
