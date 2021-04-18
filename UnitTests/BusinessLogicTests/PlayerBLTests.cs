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

        private Mock<IRepository<Playlist>> playlistRepoMock;
        private Mock<IRepository<PlayableContent>> contentRepoMock;
        private Mock<IRepository<Category>> categoryRepoMock;

        private IEnumerable<Category> categories;
        private IEnumerable<Playlist> playlists;
        private IEnumerable<PlayableContent> contents;

        private Category auxCategory;
        private PlayableContent auxPlayableContent;
        private Playlist auxPlaylist;

        private PlayerBL playerBL;

        [TestInitialize]
        public void SetUp()
        {

            auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            auxPlayableContent = new PlayableContent
            {
                Id = 1,
                Author = "Buenos Muchachos",
                Category = auxCategory,
                Duration = 1.2,
                ContentURL = "http://sin-hogar.mp3",
                ImageURL = "",
                Name = "Sin hogar"
            };
            auxPlaylist = new Playlist
            {
                Id = 1,
                Category = auxCategory,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { auxPlayableContent }
            };

            categories = new List<Category>
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
            };

            contents = new List<PlayableContent>
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
            };

            playlists = new List<Playlist>
            {
                auxPlaylist
            };

            categoryRepoMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            contentRepoMock = new Mock<IRepository<PlayableContent>>(MockBehavior.Strict);
            playlistRepoMock = new Mock<IRepository<Playlist>>(MockBehavior.Strict);

            playerBL = new PlayerBL(categoryRepoMock.Object, contentRepoMock.Object, playlistRepoMock.Object);
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            categoryRepoMock.Setup(x => x.GetAll()).Returns(categories.AsQueryable);
            List<Category> _categories = playerBL.GetCategories();

            Assert.AreEqual(4, _categories.Count);
            categoryRepoMock.VerifyAll();
        }

        [TestMethod]
        public void GetPlaylistTest()
        {
            playlistRepoMock.Setup(x => x.Get(1)).Returns(auxPlaylist);
            Playlist playlistElement = playerBL.GetPlaylist(1);

            Assert.AreEqual(1, playlistElement.Id);
            playlistRepoMock.VerifyAll();
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

            contentRepoMock.Setup(x => x.GetAll()).
                .Returns(contents.Append(newContent).AsQueryable);
            contentRepoMock.Setup(x => x.Add(newContent));
            //aca es imposible assertear porque accede 2 veces el metodo al GetAll y no se ocmo hacer que retorne distinto
            var cont = playerBL.AddIndependentContent(newContent);

            Assert.AreEqual(newContent, cont);
            contentRepoMock.VerifyAll();
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

        }

        [TestMethod]
        public void AddContentToPlaylistTest()
        {

            PlayableContent content = playerBL.GetPlayableContent(2);
            Playlist playlist = playerBL.GetPlaylist(1);

            Playlist auxPlaulist = playerBL.AddContentToPlaylist(playlist.Id, content.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
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

            playerBL.AddContentToPlaylist(auxPlaylist.Id, newContent.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
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
            playerBL.AddContentToPlaylist(auxPlaylist.Id, newContent.Id);
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

            playerBL.AddContentToPlaylist(auxPlaylist.Id, newContent.Id);


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
                CategoryId = bodyCategory.Id,
                Duration = 1.1,
                ContentURL = "http://The-flight.mp3",
                ImageURL = "",
                Name = "The flight"
            };
            Playlist auxPlaylist = new Playlist
            {
                Id = 1,
                Category = musicCategory,
                CategoryId = musicCategory.Id,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { }
            };

            playerBL.AddContentToPlaylist(auxPlaylist.Id, newContent.Id);
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
            Playlist p = new Playlist { Id = 3, Category = c, CategoryId = c.Id, Description = "asd", Name = "asd", ImageURL = "asd" };
            playerBL.AddPlaylist(p);

        }

        [TestMethod]
        public void DeleteContentTest()
        {
            playerBL.DeleteContent(2);

        }
    }
}
