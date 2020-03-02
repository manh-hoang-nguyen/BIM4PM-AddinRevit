using System.Collections.Generic;

namespace BIM4PM.DataAccess
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(int id);

        // Get an entity by int id
        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}