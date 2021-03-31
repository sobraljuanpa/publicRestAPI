using System.Linq;

using Domain;
using IDataAccess;

namespace DataAccess
{
    public class AdministratorRepository: IRepository<Administrator>
    {

        readonly Context _context;

        public AdministratorRepository(Context context)
        {
            _context = context;
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
