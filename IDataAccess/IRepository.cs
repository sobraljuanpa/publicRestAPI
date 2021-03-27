using System;
using System.Collections.Generic;
using System.Text;

namespace IDataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T dbEntity, T newEntity);
        void Delete(T entity);
    }
}
