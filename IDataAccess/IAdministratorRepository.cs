﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccess
{
    public interface IAdministratorRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(int id, T newEntity);
        void Delete(int id);
        bool Authenticate(string email, string password);
    }
}
