using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Domain;
using DataAccess;
using IDataAccess;

namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class PlaylistRepositoryTests
    {

        PlaylistRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            var auxCategory = new Category 
            { 
                Id = 3,
                Name = "Musica" 
            };
            var auxPlayableContent = new PlayableContent 
            { 
                Id = 1, 
                Author = "Buenos Muchachos", 
                Category = auxCategory, 
                Duration = 1.2, 
                ContentURL = "http://sin-hogar.mp3", 
                ImageURL = "", 
                Name = "Sin hogar" 
            };
            var data = new List<Playlist>
            {
                new Playlist 
                { 
                    Id = 1, 
                    Category = auxCategory, 
                    Description = "Rock uruguayo", 
                    ImageURL = "", 
                    Name = "Rock uruguayo", 
                    Contents = new List<PlayableContent> { auxPlayableContent } 
                }
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("BetterCalmDB")
                .Options;

            Context context = new Context(options);

            context.Database.EnsureDeleted();
            context.Set<Playlist>().AddRange(data);
            context.SaveChanges();

            repository = new PlaylistRepository(context);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var playlists = repository.GetAll();

            int actual = playlists.Count();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            string result = "Rock uruguayo";

            var playlist = repository.Get(1);

            Assert.AreEqual(result, playlist.Name);
        }

        [TestMethod]
        public void AddPlaylistTest()
        {
            var content = new PlayableContent 
            { 
                Id = 3, 
                Author = "The Smiths", 
                Duration = 3.2, 
                ContentURL = "http://this-charming-man.mp3", 
                ImageURL = "", 
                Name = "This Charming Man" 
            };
            var playlist = new Playlist 
            { 
                Id = 2, 
                Description = "Rock 80s", 
                ImageURL = "", 
                Name = "Rock 80s", 
                Contents = new List<PlayableContent> { content } 
            };

            repository.Add(playlist);

            int actual = repository.GetAll().Count();
            int result = 2;

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void UpdateContentTest()
        {
            var playlist = new Playlist 
            { 
                Id = 1,
                CategoryId = 3, 
                Description = "Rock 80s", 
                Name = "Rock 80s", 
                Contents = { } 
            };

            repository.Update(1, playlist);

            var modifiedContent = repository.Get(1);

            Assert.AreEqual(playlist.Name, modifiedContent.Name);
        }

        [TestMethod]
        public void DeleteContentTest()
        {
            repository.Delete(1);

            int actual = repository.GetAll().Count();

            Assert.AreEqual(0, actual);
        }
    }
}
