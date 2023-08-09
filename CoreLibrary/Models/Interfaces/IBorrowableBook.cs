using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models.Interfaces
{
    public interface IBorrowableBook : IHaveId, IBorrowBase
    {
        DateTime? EndTime { get; set; }
        DateTime StartTime { get; set; }
        Book Book { get; set; }
        bool IsInPossession();
    }
}
