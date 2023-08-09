using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models.Interfaces
{
    public interface IBorrowBase
    {
        int UserId { get; set; }
        int BookId { get; set; }
    }
}
