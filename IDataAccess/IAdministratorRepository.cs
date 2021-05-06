using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace IDataAccess
{
    public interface IAdministratorRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(int id, T newEntity);
        void Delete(int id);
        Administrator Authenticate(string email, string password);
    }
}
