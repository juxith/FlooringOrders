using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMasteryModels.Interfaces
{
    public interface IProductRepo
    {
        Product LoadProduct(string productType);
        List<Product> LoadListProducts();
        //void SaveAccount(Product Order);
    }
}
