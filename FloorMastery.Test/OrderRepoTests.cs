using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FloorMastery.BLL;
using FloorMasteryModels.Response;
using FloorMasteryData;
using FloorMasteryModels;

namespace FloorMastery.Test
{

    [TestFixture]
    public class OrderRepoTests
    {
        DateTime date = new DateTime(2017, 10, 10);
        
        [Test]
        public void CanLoadListOrderTestRepo()
        {
            OrderManager manager = new OrderManager(new OrdersTestRepo(), new ProductTestRepo(), new TaxTestRepo());
            DisplayOrderResponse response = manager.DisplayOrder(new DateTime(2017, 09, 19));

            Assert.IsNotNull(response.ListOfOrders);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void CanAddOrderTestRepo() //tests the LoadOrderMethod AND the load and list Methods of the Tax and Product repos
        {
            OrderManager manager = new OrderManager(new OrdersTestRepo(), new ProductTestRepo(), new TaxTestRepo());
            AddOrderResponse addOrder = manager.AddOrder(new DateTime(2017, 09, 10), "Unit Testing", "OH", "wood", 100, 0);

            Assert.IsNotNull(addOrder.Order);
            Assert.IsTrue(addOrder.Success);
            Assert.AreEqual(addOrder.Order.CustomerName, "Unit Testing");
            Assert.AreEqual(addOrder.Order.Total, 808.4m);
        }

        [TestCase("cust1", "WA", "wood", 200, false)] //fail because MN is not a valid state.
        [TestCase("cust1", "OH", "marble", 200, false)]//fail because Marble is not a valid
        [TestCase("cust1", "OH", "wood", 200, true)]
        public void CanAddOrderTest(string customerName, string stateAbbrev, string material, decimal area, bool success) //tests the LoadOrderMethod
        {
            OrderManager manager = new OrderManager(new OrdersTestRepo(), new ProductTestRepo(), new TaxTestRepo());
            AddOrderResponse addOrder = manager.AddOrder(date, customerName, stateAbbrev, material, area, 0);

            Assert.AreEqual(addOrder.Success, success);
        }

        [Test]
        public void CanSaveOrder()
        {
            OrderManager manager = new OrderManager(new OrdersTestRepo(), new ProductTestRepo(), new TaxTestRepo());

            Order order = AutoFixtureHelper.CreateOrder();

            manager.AddToOrderRepo(order);

            DisplayOrderResponse response = manager.DisplayOrder(order.OrderDate);

            Assert.IsNotNull(response.ListOfOrders);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(order.OrderNumber, 1); //Tests the GetORderNumberBasedOnDateMethod
        }

        [Test]
        public void CanPersistUpdatedOrder() //Tests the GetORderNumberBasedOnDateMethod
        {
            OrderManager manager = new OrderManager(new OrdersTestRepo(), new ProductTestRepo(), new TaxTestRepo());

            AddOrderResponse addOrder = manager.AddOrder(new DateTime(2017, 09, 10), "Unit Testing", "OH", "wood", 100, 0);

            AddOrderResponse addOrderAgain = manager.AddOrder(addOrder.Order.OrderDate, addOrder.Order.CustomerName, "MN", addOrder.Order.ProductType, addOrder.Order.Area, addOrder.Order.OrderNumber);

            manager.UpdateOrder(addOrderAgain.Order);


            Assert.IsNotNull(addOrderAgain);
            Assert.IsTrue(addOrderAgain.Success);

            Order holdingOrder = new Order();
            holdingOrder = addOrderAgain.Order;
            Assert.AreEqual(holdingOrder.State, "MN");
        }

        [Test]
        public void CanPersistOrderRemoval()
        {
            OrderManager manager = new OrderManager(new OrdersTestRepo(), new ProductTestRepo(), new TaxTestRepo());

            AddOrderResponse addResponse = manager.AddOrder(new DateTime(2017, 09, 10), "Unit Testing", "OH", "wood", 100, 0);

            manager.RemoveOrder(addResponse.Order);

            DisplayOrderResponse displayResponse = manager.DisplayOrder(new DateTime(2017, 09, 10));

            Assert.IsNotNull(displayResponse.ListOfOrders);
            Assert.AreEqual(displayResponse.ListOfOrders.Count, 0);
            Assert.IsTrue(displayResponse.Success);
        }
    }
}
