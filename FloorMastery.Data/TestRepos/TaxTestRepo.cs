using FloorMasteryModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorMasteryModels;

namespace FloorMasteryData
{
    public class TaxTestRepo : ITaxRepo
    {
        List<Tax> listOfTax = new List<Tax>()
        {
            new Tax
            {
            StateAbbreviation = "MN",
            StateName = "Minnesota",
            TaxRate = 1.075M
            },

            new Tax
            {
            StateAbbreviation = "OH",
            StateName = "Ohio",
            TaxRate = 1.05M
            },

        };

        public Tax LoadTax(string stateAbbreviation)
        {
            var tax = listOfTax.SingleOrDefault(c => c.StateAbbreviation == stateAbbreviation);
            return tax;
        }
    }
}
