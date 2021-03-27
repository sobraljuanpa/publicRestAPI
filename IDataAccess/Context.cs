using System;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace IDataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlayableContent> PlayableContents { get; set; }
    }
}
