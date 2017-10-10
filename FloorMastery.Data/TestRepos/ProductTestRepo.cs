using FloorMasteryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorMasteryModels;

namespace FloorMasteryData
{
    public class ProductTestRepo : IProductRepo
    {
        List<Product> listOfProducts = new List<Product>
        {
            new Product
            {
            ProductType = "wood",
            CostPerSquareFoot = 3.00M,
            LaborCostPerSquareFoot = 5.00M
            }
        };

        public Product LoadProduct(string productType)
        {
            var product = listOfProducts.SingleOrDefault(c => c.ProductType == productType);
            return product;
        }

        public List<Product> LoadListProducts()
        {
            return listOfProducts;
        }
    }
}
