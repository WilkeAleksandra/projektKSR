using System.Collections.Generic;

namespace DatabaseConnection.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void AddRange(List<T> entities);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
    }
}
