using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        public virtual T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public  virtual T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
