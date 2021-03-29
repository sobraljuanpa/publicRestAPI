using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;

namespace UnitTests
{
    [TestClass]
    public class PlaylistRepositoryTests
    {

        PlaylistRepository repository;
        Mock<DbSet<Playlist>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var auxPlayableContent = new PlayableContent { Id = 1, Author = "Buenos Muchachos", Category = auxCategory, Duration = 1.2, ContentURL = "http://sin-hogar.mp3", ImageURL = "", Name = "Sin hogar" };
            var data = new List<Playlist>
            {
                new Playlist { Id = 1, Category = auxCategory, Description = "Rock uruguayo", ImageURL = "", Name = "Rock uruguayo", Contents = new List<PlayableContent> { auxPlayableContent } }
            }.AsQueryable();

            mockSet = new Mock<DbSet<Playlist>>();
            mockSet.As<IQueryable<Playlist>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Playlist>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Playlist>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.Playlists).Returns(mockSet.Object);

            repository = new PlaylistRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var playlists = repository.GetAll();

            Assert.AreEqual(1, playlists.ToList().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var playlist = repository.Get(1);

            Assert.AreEqual("Rock uruguayo", playlist.Name);
        }

        [TestMethod]
        public void AddPlaylistTest()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var content = new PlayableContent { Id = 3, Author = "The Smiths", Category = auxCategory, Duration = 3.2, ContentURL = "http://this-charming-man.mp3", ImageURL = "", Name = "This Charming Man" };
            var playlist = new Playlist { Id = 2, Category = auxCategory, Description = "Rock 80s", ImageURL = "", Name = "Rock 80s", Contents = new List<PlayableContent> { content } };
            repository.Add(playlist);

            mockSet.Verify(v => v.Add(It.IsAny<Playlist>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateContentTest()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var content = new PlayableContent { Id = 3, Author = "The Smiths", Category = auxCategory, Duration = 3.2, ContentURL = "http://this-charming-man.mp3", ImageURL = "", Name = "This Charming Man" };
            var playlist = new Playlist { Id = 1, Category = auxCategory, Description = "Rock 80s", ImageURL = "", Name = "Rock 80s", Contents = new List<PlayableContent> { content } };
            repository.Update(1, playlist);
            var modifiedContent = repository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("Rock 80s", modifiedContent.Name);
        }

        [TestMethod]
        public void DeleteContentTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }
    }
}
