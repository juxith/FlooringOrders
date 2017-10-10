using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMasteryModels.Interfaces
{
    public interface IOrderRepo
    {
        List<Order> LoadListOrder(DateTime OrderDate);
        Order LoadOrder (DateTime orderDate, int orderNumber);
        void SaveOrder(Order order);
        void PersistUpdatedOrder(Order order);
        Order GetOrderNumberBasedOnDate(Order Order);
        void PersistOrderRemoval(Order order);
    }
}
