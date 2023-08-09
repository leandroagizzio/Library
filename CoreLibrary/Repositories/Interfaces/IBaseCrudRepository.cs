namespace CoreLibrary.Repositories.Interfaces
{
    public interface IBaseCrudRepository<T> where T : class, Models.Interfaces.IHaveId
    {
        T? Create(T entity);
        bool Delete(int id);
        T? Read(int id);
        IList<T> ReadAll();
        T? Update(T updatedEntity);

        T? ReadByComparison(Func<T, bool> funcComparison);
    }
}
