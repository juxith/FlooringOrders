
using FloorMasteryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorMasteryModels;

namespace FloorMasteryData
{
    public class OrdersTestRepo : IOrderRepo
    {
        static List<Order> _listOfOrders = new List<Order>
        {
            new Order
            {
                  OrderDate = new DateTime(2017,09,19),
                  OrderNumber = 1,
                  CustomerName = "Wise",
                  State = "OH",
                  TaxRate = 1.0625M,
                  ProductType = "wood",
                  Area = 100.00M,
                  CostPerSquareFoot = 5.15M,
                  LaborCostPerSquareFoot = 4.75M,
            },

            new Order
              {
                  OrderDate = new DateTime(2017,09,19),
                  OrderNumber = 2,
                  CustomerName = "Thao",
                  State = "MN",
                  TaxRate = 1.0725M,
                  ProductType = "wood",
                  Area = 100.00M,
                  CostPerSquareFoot = 5.15M,
                  LaborCostPerSquareFoot = 4.75M,
            },
        };

        public List<Order> LoadListOrder(DateTime orderDate)
        {
            var listOrder = _listOfOrders.Where(c => c.OrderDate == orderDate);

            return listOrder.ToList();
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            var order = _listOfOrders.Where(c => c.OrderDate == orderDate).SingleOrDefault(d => d.OrderNumber == orderNumber);

            return order;
        }

        public Order GetOrderNumberBasedOnDate (Order order)
        {
            var doesDateAlreadyExist = _listOfOrders.Any(date => date.OrderDate == order.OrderDate);

            if (doesDateAlreadyExist != true)
            {
                order.OrderNumber = 1;
            }
            else
            {
                //Grab a list who's date == order.Date. Then order that list by descending orderNumber. Then set order.OrderNumber to FirstOrdernumber in list + 1;

                var findHighestOrderNum = _listOfOrders.Where(date => date.OrderDate == order.OrderDate).ToList();
                var orderNum = findHighestOrderNum.OrderByDescending(c => c.OrderNumber);
                order.OrderNumber = orderNum.First().OrderNumber + 1;
            }
            return order;
        }

        public void SaveOrder(Order order)
        {
            GetOrderNumberBasedOnDate(order);
            _listOfOrders.Add(order);
        }

        public void PersistUpdatedOrder(Order order)
        {
            Order oldOrder = LoadOrder(order.OrderDate, order.OrderNumber);
            _listOfOrders.Remove(oldOrder);
            _listOfOrders.Add(order);
        }

        public void PersistOrderRemoval(Order order)
        {
            LoadOrder(order.OrderDate, order.OrderNumber);
            _listOfOrders.Remove(order);
        }
    }
}
