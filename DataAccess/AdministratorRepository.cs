using System.Linq;

using Domain;
using IDataAccess;

namespace DataAccess
{
    public class AdministratorRepository: IAdministratorRepository<Administrator>
    {

        readonly Context _context;

        public AdministratorRepository(Context context)
        {
            _context = context;
        }

        public bool Authenticate(string email, string password)
        {
            var authenticates = _context.Administrators.SingleOrDefault(
                x => x.Email == email && x.Password == password);

            if (authenticates != null) return true;

            return false;
        }

        public IQueryable<Administrator> GetAll()
        {
            return _context.Administrators;
        }

        public Administrator Get(int id)
        {
            return _context.Administrators.Find(id);
        }

        public void Add(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
        }

        public void Update(int id, Administrator administrator)
        {
            Get(id).Email = administrator.Email;
            Get(id).Name = administrator.Name;
            Get(id).Password = administrator.Password;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Administrators.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
