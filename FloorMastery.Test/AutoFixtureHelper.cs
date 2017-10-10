using FloorMasteryModels;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMastery.Test
{
    public static class AutoFixtureHelper
    {
        public static Fixture fixture = new Fixture(); 

        public static DateTime CreateDateTime(string dateTime)
        {
            DateTime parsedDateTime = new DateTime();
            DateTime.TryParse(dateTime, out parsedDateTime);
            return parsedDateTime;
        }

        public static Order CreateOrder()
        {
            Order order = fixture.Create<Order>();
            order.OrderDate = new DateTime(2017, 10, 10);
            order.ProductType = "wood";
            order.State = "OH";
            order.Area = 100;
            return order;
        }
    }
}
