using CoreLibrary.Data;
using CoreLibrary.Helper.ExtensionMethods;
using CoreLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Repositories
{
    public class BaseCrudRepository<T> : IBaseCrudRepository<T> where T : class, Models.Interfaces.IHaveId
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public BaseCrudRepository(Context context, DbSet<T> dbSet) {
            _context = context;
            _dbSet = dbSet;
        }

        public virtual T? Create(T entity) {
            _context.Add(entity);

            if (_context.SaveChanges() <= 0)
                return null;

            return entity;
        }

        public T? Read(int id) {
            T? entity = _dbSet.FirstOrDefault(x => x.Id == id);// ?? throw new Exception("Id not found");
            return entity;
        }

        public IList<T> ReadAll() {
            return _dbSet.ToList();
        }

        public virtual T? Update(T updatedEntity) {
            return Update(updatedEntity, (a, b) => a.CopyToMeNoId(b));
        }

        public virtual T? Update(T updatedEntity, Action<T, T> action) {
            var entityDB = Read(updatedEntity.Id);

            if (entityDB == null)
                return null;

            //entityDB.CopyToMe(updatedEntity);
            action.Invoke(entityDB, updatedEntity);

            _context.Update(entityDB);

            if (_context.SaveChanges() <= 0)
                return null;

            return entityDB;
        }

        public bool Delete(int id) {
            T? entityDB = Read(id);

            if (entityDB == null)
                return false;

            _context.Remove(entityDB);

            if (_context.SaveChanges() <= 0)
                return false;

            return true;
        }

        public T? ReadByComparison(Func<T, bool> funcComparison) {
            return _dbSet.AsEnumerable().FirstOrDefault(x => funcComparison(x));
        }
    }
}
