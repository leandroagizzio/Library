using CoreLibrary.Data;
using CoreLibrary.Helper.ExtensionMethods;
using CoreLibrary.Models;
using CoreLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Repositories
{
    public class UserRepository : BaseCrudRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(Context context) : base(context, context.Users) {
            _dbSet = context.Users;
        }

        public override User? Update(User updatedEntity) {
            return base.Update(updatedEntity, (a,b) => a.CopyToMeExcludeList(b, "Id", "Password"));
        }

        public User? ReadByLogin(string login) {
            return base.ReadByComparison(u => string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase));
        }

        public override User? Create(User entity) {
            entity.SetHashPassword();
            return base.Create(entity);
        }
    }
}
