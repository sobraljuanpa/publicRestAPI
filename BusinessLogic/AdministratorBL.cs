using Domain;
using IBusinessLogic;
using IDataAccess;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessLogic 
{

    public class AdministratorBL : IAdministratorBL
    {

        private readonly IAdministratorRepository<Administrator> administratorRepository;

        public AdministratorBL(IAdministratorRepository<Administrator> administratorRepository)
        {
            this.administratorRepository = administratorRepository;
        }

        public Administrator Authenticate(string email, string password)
        {
            return administratorRepository.Authenticate(email, password);
        }

        private bool ValidEmail(string email)
        {
            bool isValid = true;
            List<Administrator> administrators = administratorRepository.GetAll().ToList();
            
            foreach(Administrator admin in administrators)
            {
                if (admin.Email.ToLower() == email.ToLower()) isValid = false; 
            }

            return isValid;
        }

        public void AddAdministrator(Administrator administrator)
        {
            if(ValidEmail(administrator.Email))
            {
                administratorRepository.Add(administrator);
            }
            else
            {
                throw new System.Exception("The email you are trying to use is already associated to an administrator");
            }
        }

        public void DeleteAdministrator(int id)
        {
            if(id <= administratorRepository.GetAll().ToList().Count() && id > 0)
            {
                administratorRepository.Delete(id);
            }
            else throw new Exception("No content associated to given id");
            
        }

        public void UpdateAdministrator(int id, Administrator administrator)
        {
            administratorRepository.Update(id, administrator);
        }

        public Administrator Get (int id)
        {
            if (id != 0)
            {
                return administratorRepository.Get(id);
            }
            else throw new Exception("No administrator associated to given id.");
        }

    }
        
}
