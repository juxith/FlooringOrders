using FloorMastery.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FloorMasteryData;
using FloorMasteryData.FileRepos;

namespace FloorMastery.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            //App.Settings is a dictionary.
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestRepo":
                    return new OrderManager(new OrdersTestRepo(),new ProductTestRepo(), new TaxTestRepo());
                case "FileRepo":
                    return new OrderManager(//new OrderFileRepo(@"C:\Repos\dotnet-judy-thao\FloorMastery\Orders"),
                                            new OrderFileRepo(@"C:\Repos\dotnet-judy-thao\FloorMasteryFix\SeedOrders"),
                                            new ProductFileRepo(@"C:\Repos\dotnet-judy-thao\FloorMasteryFix\Products.txt"),
                                            new TaxFileRepo(@"C:\Repos\dotnet-judy-thao\FloorMasteryFix\Taxes.txt"));
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        } 
    }
}
