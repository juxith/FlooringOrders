using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMasteryModels.Response
{
    public class AddOrderResponse : Response
    {
        public Order Order { get; set; }
        public Tax StateTax { get; set; }
        public Product ProductType { get; set; }
    }
}
