using System.Linq;

using Domain;
using IDataAccess;

namespace DataAccess
{
    public class VideoContentRepository : IRepository<VideoContent>
    {
        readonly Context _context;

        public VideoContentRepository(Context context)
        {
            _context = context;
        }

        public void Add(VideoContent entity)
        {
            _context.VideoContents.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.VideoContents.Remove(Get(id));
            _context.SaveChanges();
        }

        public VideoContent Get(int id)
        {
            return _context.VideoContents.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<VideoContent> GetAll()
        {
            return _context.VideoContents;
        }

        public void Update(int id, VideoContent newEntity)
        {
            Get(id).Name = newEntity.Name;
            Get(id).Duration = newEntity.Duration;
            Get(id).CategoryId = newEntity.CategoryId;
            Get(id).Author = newEntity.Author;
            Get(id).VideoURL = newEntity.VideoURL;
            _context.SaveChanges();
        }
    }
}
