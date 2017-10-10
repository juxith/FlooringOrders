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
    public class OrderFileRepo : IOrderRepo
    {
        private static string _path;

        private static string _pathFileBeforeTextFile; //technically don't need because it could be hardcoded in write to text file

        private List<Order> _listOfOrders = new List<Order>();

        public OrderFileRepo(string path)
        {
            _path = path;
            CreateListOfOrders(_path);
        }

        public void CreateListOfOrders(string path)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(_path, "*.txt"))
                {
                    //string contents = File.ReadAllText(file);

                    using (StreamReader orderReader = new StreamReader(file))
                    {
                        orderReader.ReadLine();

                        for (string line = orderReader.ReadLine(); line != null; line = orderReader.ReadLine())
                        {
                            string[] cells = line.Replace("\"", "").Split(',');
                            Order order = new Order();
                            order.OrderDate = OrderDateExtractedFromFileName(file);
                            order.OrderNumber = int.Parse(cells[0]);
                            order.CustomerName = (cells[1].Contains('*')? cells[1].Replace('*', ','): cells[1]);
                            order.State = cells[2];
                            order.TaxRate = decimal.Parse(cells[3]);
                            order.ProductType = cells[4].ToLower();
                            order.Area = decimal.Parse(cells[5]);
                            order.CostPerSquareFoot = decimal.Parse(cells[6]);
                            order.LaborCostPerSquareFoot = decimal.Parse(cells[7]);

                            _listOfOrders.Add(order);
                        }
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

        public DateTime OrderDateExtractedFromFileName(string _path)
        {
            string[] splitByUnderBar = _path.Split('_');
            string[] splitByPeriod = splitByUnderBar[1].Split('.'); //could use remove.

            _pathFileBeforeTextFile = splitByUnderBar[0];

            string formatDate = splitByPeriod[0].Insert(2, "/").Insert(5, "/");

            return DateTime.Parse(formatDate);
        }

        public Order GetOrderNumberBasedOnDate(Order order)
        {
            var doesDateAlreadyExist = _listOfOrders.Any(date => date.OrderDate == order.OrderDate);

            if (doesDateAlreadyExist != true)
            {
                order.OrderNumber = 1;
            }
            else
            {
                var findHighestOrderNum = _listOfOrders.Where(date => date.OrderDate == order.OrderDate).ToList();
                var orderNum = findHighestOrderNum.OrderByDescending(c => c.OrderNumber);
                order.OrderNumber = orderNum.First().OrderNumber + 1;
            }
            return order;
        }

        public List<Order> LoadListOrder(DateTime orderDate)
        {
            var listOrder = _listOfOrders.Where(c => c.OrderDate == orderDate).OrderBy(o => o.OrderNumber);

            return listOrder.ToList();
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            var order = _listOfOrders.Where(c => c.OrderDate == orderDate).SingleOrDefault(d => d.OrderNumber == orderNumber);

            return order;
        }

        public void PersistOrderRemoval(Order order)
        {
            LoadOrder(order.OrderDate, order.OrderNumber);
            _listOfOrders.Remove(order);
            WriteToTxtFile(order);
        }

        public void PersistUpdatedOrder(Order order)
        {
            Order oldOrder = LoadOrder(order.OrderDate, order.OrderNumber);
            _listOfOrders.Remove(oldOrder);
            _listOfOrders.Add(order);
            WriteToTxtFile(order);
        }

        public void SaveOrder(Order order)
        {
            GetOrderNumberBasedOnDate(order);
            _listOfOrders.Add(order);
            WriteToTxtFile(order);
        }

        public void WriteToTxtFile(Order order)
        {
            string pathToWrite = _pathFileBeforeTextFile + "_" + FormatDateTime(order) + ".txt";
            if (File.Exists(pathToWrite))
            {
                File.Delete(pathToWrite);
            }

            using (StreamWriter sw = new StreamWriter(pathToWrite))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                List<Order> listToWriteBack = LoadListOrder(order.OrderDate);

                foreach (var item in listToWriteBack)
                {
                    sw.WriteLine(CreateTextForOrder(item));
                }
            }
        }

        public string FormatDateTime(Order order)
        {
            return order.OrderDate.ToString("MMddyyyy");
        }

        public string CreateTextForOrder(Order order)
        {
            order.CustomerName = (order.CustomerName.Contains(',') ? order.CustomerName.Replace(',', '*'): order.CustomerName);
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", order.OrderNumber, order.CustomerName, order.State, order.TaxRate, order.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
        }
    }
}
