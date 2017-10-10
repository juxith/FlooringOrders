using FloorMasteryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorMasteryModels;
using System.IO;

namespace FloorMasteryData.FileRepos
{
    public class ProductFileRepo : IProductRepo
    {
        private static string _path;

        private List<Product> listOfProduct = new List<Product>();

        public ProductFileRepo(string path)
        {
            _path = path;
            CreateListOfStates();
        }

        public void CreateListOfStates()
        {
            try
            {
                using (StreamReader productReader = new StreamReader(_path))
                {
                    productReader.ReadLine();

                    for (string line = productReader.ReadLine(); line != null; line = productReader.ReadLine())
                    {
                        string[] cells = line.Replace("\"", "").Split(',');
                        Product product = new Product();

                        product.ProductType = cells[0].ToLower();
                        product.CostPerSquareFoot = decimal.Parse(cells[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(cells[2]);

                        listOfProduct.Add(product);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file: {_path} was not found.");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public List<Product> LoadListProducts()
        {
            return listOfProduct;
        }

        public Product LoadProduct(string productType)
        {
            var loadProduct = listOfProduct.SingleOrDefault(acc => acc.ProductType == productType);

            return loadProduct;
        }
    }
}
