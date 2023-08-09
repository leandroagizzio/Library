using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models.Interfaces
{
    public interface IUser : ICrudModel
    {
        bool IsAdmin { get; set; }
        string Email { get; set; }
        string Login { get; set; }

        bool ValidatePassword(string password);
    }
}
