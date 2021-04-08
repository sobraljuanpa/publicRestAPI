﻿using Domain;

namespace IBusinessLogic
{
    public interface IAdministratorBL
    {
        public Administrator Authenticate(string email, string password);

        public void AddAdministrator(Administrator administrator);

        public void DeleteAdministrator(int id);

        public void UpdateAdministrator(int id, Administrator administrator);
        public Administrator Get(int id);
    }
}
