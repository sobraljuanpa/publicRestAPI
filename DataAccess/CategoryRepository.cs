using System.Linq;

using Domain;
using IDataAccess;

namespace DataAccess
{
    public class CategoryRepository : IRepository<Category>
    {
        readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category Get(int id)
        {
            //TODO determinar por que no anda esto y si lo de abajo
            //return _context.Categories.FirstOrDefault(c => c.Id == id);
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(int id, Category category)
        {
            Get(id).Name =category.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Categories.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
