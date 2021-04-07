using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using WebAPI.Controllers;
using DataAccess;
using IDataAccess;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTests
{
    [TestClass]
    public class PlaylistControllerTests
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
        PlaylistController controller;

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
                CategoryId = auxCategory.Id,
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
                    CategoryId = auxCategory.Id,
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
                  CategoryId = auxCategory.Id,
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
                    CategoryId = auxCategory.Id,
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
            controller = new PlaylistController(playerBL);
        }

        [TestMethod]
        public void GetPlaylistByIdTest()
        {
            var result = controller.GetPlaylistById(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetNonExistantPlaylistByIdTest()
        {
            var result = controller.GetPlaylistById(-1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public void AddPlaylistTest()
        {
            Category c = new Category { Id = 3, Name = "Musica"};
            PlayableContent content = new PlayableContent { 
                Id = 3,
                Author = "The smiths",
                Category = c,
                CategoryId = c.Id,
                Duration = 1.2,
                ContentURL = "http://this-charming-man.mp3",
                ImageURL = "",
                Name = "This charming man" };

            Playlist p = new Playlist { 
                Id = 2,
                Category = c,
                CategoryId = c.Id,
                Description = "Best of 80s rock", 
                ImageURL = "", 
                Name = "Alternative rock", 
                Contents = new List<PlayableContent> { content } };

            var result = controller.CreatePlaylist(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
        }

        [TestMethod]
        public void AddExistingPlaylistTest ()
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
                CategoryId = auxCategory.Id,
                Duration = 1.2,
                ContentURL = "http://sin-hogar.mp3",
                ImageURL = "",
                Name = "Sin hogar"
            };

            Playlist p = new Playlist
            {
                Id = 1,
                Category = auxCategory,
                CategoryId = auxCategory.Id,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { auxPlayableContent }
            };

            var result = controller.CreatePlaylist(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);

        }

        [TestMethod]
        public void DeletePlaylistByIdTest()
        {
            var result = controller.DeletePlaylistById(1);
            var objectResult = result as NoContentResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(204, statusCode);
        }

        [TestMethod]
        public void DeleteInvalidPlaylistByIdTest()
        {
            var result = controller.DeletePlaylistById(30);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
        }
    }
}
