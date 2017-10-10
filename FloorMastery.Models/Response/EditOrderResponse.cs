using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMasteryModels.Response
{
    public class EditOrderResponse : Response
    {
        public Order Order { get; set; }
        public List<Order> ListOfOrders { get; set; }
    }
}
