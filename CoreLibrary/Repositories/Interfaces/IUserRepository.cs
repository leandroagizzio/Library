using CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Repositories.Interfaces
{
    public interface IUserRepository : IBaseCrudRepository<User>
    {
        User? ReadByLogin(string login);
    }
}
