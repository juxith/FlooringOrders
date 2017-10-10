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
    public class TaxFileRepo : ITaxRepo
    {
        private static string _path;

        private List<Tax> listOfTax = new List<Tax>();

        public TaxFileRepo(string path)
        {
            _path = path;
            CreateListOfStates();
        }

        public void CreateListOfStates()
        {
            try
            {
                using (StreamReader taxReader = new StreamReader(_path))
                {
                    taxReader.ReadLine();

                    for (string line = taxReader.ReadLine(); line != null; line = taxReader.ReadLine())
                    {
                        string[] cells = line.Replace("\"", "").Split(',');
                        Tax tax = new Tax();
                        tax.StateAbbreviation = cells[0].ToUpper();
                        tax.StateName = cells[1];
                        tax.TaxRate = decimal.Parse(cells[2]);

                        listOfTax.Add(tax);
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

        public Tax LoadTax(string stateAbbreviation)
        {
            var loadTax = listOfTax.SingleOrDefault(acc => acc.StateAbbreviation == stateAbbreviation);

            return loadTax;
        }
    }
}
