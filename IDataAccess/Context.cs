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

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlayableContent> PlayableContents { get; set; }
    }
}
