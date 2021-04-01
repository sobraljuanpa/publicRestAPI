using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic 
{

    public class AdministratorBL : IAdministratorBL
    {

        private readonly IAdministratorRepository<Administrator> administratorRepository;

        public AdministratorBL(IAdministratorRepository<Administrator> administratorRepository)
        {
            this.administratorRepository = administratorRepository;
        }

        public bool Authenticate(string email, string password)
        {
            return administratorRepository.Authenticate(email, password);
        }

        public void AddAdministrator(Administrator administrator)
        {
            administratorRepository.Add(administrator);
        }

        public void DeleteAdministrator(int id)
        {
            administratorRepository.Delete(id);
        }

        public void UpdateAdministrator(int id, Administrator administrator)
        {
            administratorRepository.Update(id, administrator);
        }

    }
        
}
