using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;
using BusinessLogic;

namespace UnitTests.BusinessLogicTests
{
    public class PlayerBLTests
    {

        Mock<DbSet<PlayableContent>> contentMockSet;
        Mock<DbSet<Category>> categoryMockSet;
        Mock<DbSet<Playlist>> playlistMockSet;
        PlayableContentRepository playableContentRepository;
        CategoryRepository categoryRepository;
        PlaylistRepository playlistRepository;
        PlayerBL playerBL;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestInitialize]
        public void SetUp()
        {

            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var auxPlayableContent = new PlayableContent { Id = 1, Author = "Buenos Muchachos", Category = auxCategory, Duration = 1.2, ContentURL = "http://sin-hogar.mp3", ImageURL = "", Name = "Sin hogar" };

            var dataCategory = new List<Category>
            {
                new Category {Id = 1, Name = "Dormir"},
                new Category {Id = 2, Name = "Meditar"},
                new Category {Id = 3, Name = "Musica"},
                new Category {Id = 4, Name = "Cuerpo"},
            }.AsQueryable();

            var dataPlayableContent = new List<PlayableContent>
            {
                new PlayableContent { Id = 1, Author = "Buenos Muchachos", Category = auxCategory, Duration = 1.2, ContentURL = "http://sin-hogar.mp3", ImageURL = "", Name = "Sin hogar"},
                new PlayableContent { Id = 2, Author = "Buitres", Category = auxCategory, Duration = 2.2, ContentURL = "http://cadillac-solitario.mp3", ImageURL = "", Name = "Cadillac solitario"}
            }.AsQueryable();

            var dataPlaylist = new List<Playlist>
            {
                new Playlist { Id = 1, Category = auxCategory, Description = "Rock uruguayo", ImageURL = "", Name = "Rock uruguayo", Contents = new List<PlayableContent> { auxPlayableContent } }
            }.AsQueryable();

            categoryMockSet = new Mock<DbSet<Category>>();
            categoryMockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(dataCategory.Expression);
            categoryMockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(dataCategory.ElementType);
            categoryMockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(dataCategory.GetEnumerator());
            categoryMockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => dataCategory.FirstOrDefault(d => d.Id == (int)pk[0]));

            contentMockSet = new Mock<DbSet<PlayableContent>>();
            contentMockSet.As<IQueryable<PlayableContent>>().Setup(m => m.Expression).Returns(dataPlayableContent.Expression);
            contentMockSet.As<IQueryable<PlayableContent>>().Setup(m => m.ElementType).Returns(dataPlayableContent.ElementType);
            contentMockSet.As<IQueryable<PlayableContent>>().Setup(m => m.GetEnumerator()).Returns(dataPlayableContent.GetEnumerator());
            contentMockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => dataPlayableContent.FirstOrDefault(d => d.Id == (int)pk[0]));

            playlistMockSet = new Mock<DbSet<Playlist>>();
            playlistMockSet.As<IQueryable<Playlist>>().Setup(m => m.Expression).Returns(dataPlaylist.Expression);
            playlistMockSet.As<IQueryable<Playlist>>().Setup(m => m.ElementType).Returns(dataPlaylist.ElementType);
            playlistMockSet.As<IQueryable<Playlist>>().Setup(m => m.GetEnumerator()).Returns(dataPlaylist.GetEnumerator());
            playlistMockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => dataPlaylist.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);

            mockContext.Setup(v => v.PlayableContents).Returns(contentMockSet.Object);
            playableContentRepository = new PlayableContentRepository(mockContext.Object);

            mockContext.Setup(v => v.Categories).Returns(categoryMockSet.Object);
            categoryRepository = new CategoryRepository(mockContext.Object);

            mockContext.Setup(v => v.Playlists).Returns(playlistMockSet.Object);
            playlistRepository = new PlaylistRepository(mockContext.Object);


            playerBL = new PlayerBL(categoryRepository,playableContentRepository,playlistRepository);
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            List<Category> categories = playerBL.GetCategories();

            Assert.AreEqual(4, categories.Count);
        }

        [TestMethod]
        public void GetPlaylistTest()
        {
            Playlist playlistElement = playerBL.GetPlaylist(1);

            Assert.AreEqual(1, playlistElement.Id);
        }

        [TestMethod]
        public void AddIndependentContentTest()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            PlayableContent newContent = new PlayableContent { Id = 3, Author = "Cuatro Pesos de Propina", Category = auxCategory, Duration = 4.4, ContentURL = "http://mi-revolucion.mp3", ImageURL = "", Name = "Mi Revolución" };
;
            playerBL.AddIndependentContent(newContent);

            contentMockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void AddContentToPlaylistTest(int contentId)
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            PlayableContent newContent = new PlayableContent { Id = 3, Author = "Cuatro Pesos de Propina", Category = auxCategory, Duration = 4.4, ContentURL = "http://mi-revolucion.mp3", ImageURL = "", Name = "Mi Revolución" };

            playerBL.AddContentToPlaylist(1,newContent.Id);
            

            playlistMockSet.Verify(v => v.Add(It.IsAny<Playlist>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());

        }
    }
}
