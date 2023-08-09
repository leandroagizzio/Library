using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models.Interfaces
{
    public interface IBook : ICrudModel
    {
        string Author { get; set; }
        int DaysToBorrow { get; set; }
    }
}
