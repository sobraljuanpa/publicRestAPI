using System.Linq;

using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PlaylistRepository : IRepository<Playlist>
    {
        readonly Context _context;

        public PlaylistRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Playlist> GetAll()
        {
            return _context.Playlists
                .Include(p => p.Category)
                .Include(p => p.Contents);
        }

        public Playlist Get(int id)
        {
            //TODO determinar por que no anda esto y si lo de abajo
            //return _context.Categories.FirstOrDefault(c => c.Id == id);
            return _context.Playlists.Find(id);
        }

        public void Add(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
        }

        public void Update(int id, Playlist playlist)
        {
            Get(id).Contents = playlist.Contents;
            Get(id).Category = playlist.Category;
            Get(id).Description = playlist.Description;
            Get(id).ImageURL = playlist.ImageURL;
            Get(id).Name = playlist.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Playlists.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
