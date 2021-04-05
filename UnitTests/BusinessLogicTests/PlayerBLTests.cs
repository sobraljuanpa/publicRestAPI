using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;
using BusinessLogic;
using System;

namespace UnitTests.BusinessLogicTests
{
    [TestClass]
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

            var dataCategory = new List<Category>
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
                },
            }.AsQueryable();

            var dataPlayableContent = new List<PlayableContent>
            {
                new PlayableContent
                {
                    Id = 1,
                    Author = "Buenos Muchachos",
                    Category = auxCategory,
                    Duration = 1.2,
                    ContentURL = "http://sin-hogar.mp3",
                    ImageURL = "",
                    Name = "Sin hogar"
                },
                new PlayableContent
                {
                  Id = 2,
                  Author = "Buitres",
                  Category = auxCategory,
                  Duration = 2.2,
                  ContentURL = "http://cadillac-solitario.mp3",
                  ImageURL = "",
                  Name = "Cadillac solitario"
                }
            }.AsQueryable();

            var dataPlaylist = new List<Playlist>
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


            playerBL = new PlayerBL(categoryRepository, playableContentRepository, playlistRepository);
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
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            PlayableContent newContent = new PlayableContent {
                Author = "Cuatro Pesos de Propina",
                Category = auxCategory,
                Duration = 4.4,
                ContentURL = "http://mi-revolucion.mp3",
                ImageURL = "",
                Name = "Mi Revolución"
            };
            playerBL.AddIndependentContent(newContent);

            contentMockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddExistingIndependentContentTest()
        {
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            PlayableContent newContent = new PlayableContent
            {
                Id = 1,
                Author = "Buenos Muchachos",
                Category = auxCategory,
                Duration = 1.2,
                ContentURL = "http://sin-hogar.mp3",
                ImageURL = "",
                Name = "Sin hogar"
            };

            playerBL.AddIndependentContent(newContent);

            contentMockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());

        }

        [TestMethod]
        public void AddContentToPlaylistTest()
        {
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            PlayableContent newContent = new PlayableContent
            {
                Author = "Cuatro Pesos de Propina",
                Category = auxCategory,
                Duration = 4.4,
                ContentURL = "http://mi-revolucion.mp3",
                ImageURL = "", Name = "Mi Revolución"
            };

            Playlist auxPlaylist = new Playlist
            {
                Id = 1,
                Category = auxCategory,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { }
            };

            playerBL.AddIndependentContent(newContent);
            playerBL.AddContentToPlaylist(auxPlaylist, newContent);

            contentMockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Exactly(2));
            mockContext.Verify(e => e.SaveChanges(), Times.Exactly(3));

        }

        [TestMethod]
        public void AddNotExistingContentToPlaylistTest()
        {
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            PlayableContent newContent = new PlayableContent
            {
                Id = 4,
                Author = "Once Tiros",
                Category = auxCategory,
                Duration = 3.0,
                ContentURL = "http://Maldición.mp3",
                ImageURL = "",
                Name = "Maldición"
            };

            Playlist auxPlaylist = new Playlist
            {
                Id = 1,
                Category = auxCategory,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { }
            };

            playableContentRepository.Add(newContent);
            playerBL.AddContentToPlaylist(auxPlaylist, newContent);

            contentMockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Exactly(2));
            mockContext.Verify(e => e.SaveChanges(), Times.Exactly(3));
        }

        [TestMethod]
        public void AddContentToNotExistingPlaylistTest()
        {
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            PlayableContent newContent = new PlayableContent
            {
                Author = "Catupecu Machu",
                Category = auxCategory,
                Duration = 4.2,
                ContentURL = "http://A-veces-vuelvo.mp3",
                ImageURL = "",
                Name = "A veces vuelvo"
            };

            Playlist auxPlaylist = new Playlist
            {
                Id = 2,
                Category = auxCategory,
                Description = "Rock argentino",
                ImageURL = "",
                Name = "Rock argentino",
                Contents = new List<PlayableContent> { }
            };

            playerBL.AddIndependentContent(newContent);
            playerBL.AddContentToPlaylist(auxPlaylist, newContent);

            playlistMockSet.Verify(v => v.Add(It.IsAny<Playlist>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Exactly(3));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddExistingContentToPlaylistTest()
        {
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            PlayableContent newContent = new PlayableContent
            {
                Id = 4,
                Author = "Once Tiros",
                Category = auxCategory,
                Duration = 3.0,
                ContentURL = "http://Maldición.mp3",
                ImageURL = "",
                Name = "Maldición"
            };

            Playlist auxPlaylist = new Playlist
            {
                Id = 1,
                Category = auxCategory,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { newContent }
            };

            playerBL.AddContentToPlaylist(auxPlaylist, newContent);

            playlistMockSet.Verify(v => v.Add(It.IsAny<Playlist>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddContentToPlaylistWithDifferentCategoriesTest()
        {

            var bodyCategory = new Category
            {
                Id = 4,
                Name = "Cuerpo"
            };
            var musicCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };

            PlayableContent newContent = new PlayableContent
            {
                Id = 3,
                Author = "Peter Scherer",
                Category = bodyCategory,
                Duration = 1.1,
                ContentURL = "http://The-flight.mp3",
                ImageURL = "",
                Name = "The flight"
            };
            Playlist auxPlaylist = new Playlist
            {
                Id = 1,
                Category = musicCategory,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { }
            };

            playerBL.AddContentToPlaylist(auxPlaylist, newContent);

            contentMockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetCategoryElementsTest()
        {
            var contents = playerBL.GetCategoryElements(3);

            Assert.AreEqual(3, contents.Count);
        }

        [TestMethod]
        public void GetPlayableContentTest()
        {
            var content = playerBL.GetPlayableContent(1);

            Assert.AreEqual("Buenos Muchachos", content.Author);
        }

        [TestMethod]
        public void AddPlaylistTest()
        {
            Category c = new Category { Id = 1, Name = "q" };
            Playlist p = new Playlist { Id = 3, Category = c, Description = "asd", Name = "asd", ImageURL = "asd" };
            playerBL.AddPlaylist(p);

            playlistMockSet.Verify(v => v.Add(It.IsAny<Playlist>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeleteContentTest()
        {
            playerBL.DeleteContent(2);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }
    }
}
