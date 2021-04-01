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
        public virtual DbSet<Problem> Problems { get; set; }
        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Consultation> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasIndex(a => a.Email).IsUnique();
            modelBuilder.Entity<Administrator>().HasData(new Administrator
            {
                Id = 1,
                Password = "admin",
                Email = "admin@admin.admin"
            });
        }
    }
}
