using System.Linq;

using Domain;
using IDataAccess;

namespace DataAccess
{
    public class PlayableContentRepository : IRepository<PlayableContent>
    {
        readonly Context _context;

        public PlayableContentRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<PlayableContent> GetAll()
        {
            return _context.PlayableContents;
        }

        public PlayableContent Get(int id)
        {
            //TODO determinar por que no anda esto y si lo de abajo
            //return _context.Categories.FirstOrDefault(c => c.Id == id);
            return _context.PlayableContents.FirstOrDefault(p => p.Id == id);
        }

        public void Add(PlayableContent content)
        {
            _context.PlayableContents.Add(content);
            _context.SaveChanges();
        }

        public void Update(int id, PlayableContent content)
        {
            Get(id).Author = content.Author;
            Get(id).CategoryId = content.CategoryId;
            Get(id).ContentURL = content.ContentURL;
            Get(id).Duration = content.Duration;
            Get(id).ImageURL = content.ImageURL;
            Get(id).Name = content.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.PlayableContents.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
