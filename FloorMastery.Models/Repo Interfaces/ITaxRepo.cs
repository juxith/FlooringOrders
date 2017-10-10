using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMasteryModels.Interfaces
{
    public interface ITaxRepo
    {
        Tax LoadTax(string stateAbbreviation);
    }
}
