using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlayableContent> PlayableContents { get; set; }
        public virtual DbSet<VideoContent> VideoContents { get; set; }
        public virtual DbSet<Problem> Problems { get; set; }
        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Consultation> Consultations { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasIndex(a => a.Email).IsUnique();
            modelBuilder.Entity<Administrator>().HasData(new Administrator
            {
                Id = 1,
                Password = "admin",
                Email = "admin@admin.admin"
            });
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
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
            modelBuilder.Entity<Problem>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Problem>().HasData(new List<Problem>
            {
                new Problem 
                { 
                    Id = 1, 
                    Name= "Depresión" 
                },
                new Problem 
                { 
                    Id = 2, 
                    Name= "Estrés" 
                },
                new Problem 
                { 
                    Id = 3, 
                    Name= "Ansiedad" 
                },
                new Problem 
                { 
                    Id = 4, 
                    Name= "Autoestima" 
                },
                new Problem 
                { 
                    Id = 5, 
                    Name= "Enojo" 
                },
                new Problem 
                { 
                    Id = 6, 
                    Name= "Relaciones" 
                },
                new Problem 
                { 
                    Id = 7, 
                    Name= "Duelo" 
                },
                new Problem 
                { 
                    Id = 8, 
                    Name= "Y más" 
                }
            });
        }
    }
}
