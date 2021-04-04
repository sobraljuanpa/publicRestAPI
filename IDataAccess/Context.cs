using System.Collections.Generic;
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
            modelBuilder.Entity<Category>().HasData(new List<Category> 
            {
                new Category
                {
                    Id = 1,
                    Name = "Dormir"
                },
                new Category
                {
                    Id = 2,
                    Name = "Meditar"
                },
                new Category
                {
                    Id = 3,
                    Name = "Musica"
                },
                new Category
                {
                    Id = 4,
                    Name = "Cuerpo"
                }
            });
        }
    }
}
