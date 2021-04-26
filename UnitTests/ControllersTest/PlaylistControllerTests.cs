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
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTests
{
    [TestClass]
    public class PlaylistControllerTests
    {
        private Mock<IPlayerBL> mock;
        private Category category;
        private PlayableContent content;
        private Playlist playlist;
        private IEnumerable<Category> categories;
        private IEnumerable<PlayableContent> contents;
        private IEnumerable<Playlist> playlists;
        private PlaylistController controller;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IPlayerBL>(MockBehavior.Strict);

            category = new Category
            {
                Id = 3,
                Name = "Musica"
            };
            content = new PlayableContent
            {
                Id = 1,
                Author = "Buenos Muchachos",
                Category = category,
                CategoryId = category.Id,
                Duration = 1.2,
                ContentURL = "http://sin-hogar.mp3",
                ImageURL = "",
                Name = "Sin hogar"
            };
            playlist = new Playlist
            {
                Id = 1,
                Category = category,
                CategoryId = category.Id,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { content }
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
            }.AsQueryable();

            contents = new List<PlayableContent>
            {
                new PlayableContent
                {
                    Id = 1,
                    Author = "Buenos Muchachos",
                    Category = category,
                    CategoryId = category.Id,
                    Duration = 1.2,
                    ContentURL = "http://sin-hogar.mp3",
                    ImageURL = "",
                    Name = "Sin hogar"
                },
                new PlayableContent
                {
                  Id = 2,
                  Author = "Buitres",
                  Category = category,
                  CategoryId = category.Id,
                  Duration = 2.2,
                  ContentURL = "http://cadillac-solitario.mp3",
                  ImageURL = "",
                  Name = "Cadillac solitario"
                }
            }.AsQueryable();

            playlists = new List<Playlist>
            {
                new Playlist
                {
                    Id = 1,
                    Category = category,
                    CategoryId = category.Id,
                    Description = "Rock uruguayo",
                    ImageURL = "",
                    Name = "Rock uruguayo",
                    Contents = new List<PlayableContent> { content }
                }
            }.AsQueryable();

            controller = new PlaylistController(mock.Object);

        }

        [TestMethod]
        public void GetPlaylistByIdTest()
        {
            mock.Setup(x => x.GetPlaylist(1)).Returns(playlist);
            var result = controller.GetPlaylistById(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetNonExistantPlaylistByIdTest()
        {
            mock.Setup(x => x.GetPlaylist(-1)).Throws(new NullReferenceException());
            var result = controller.GetPlaylistById(-1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddPlaylistTest()
        {

            PlayableContent auxContent = new PlayableContent { 
                Id = 3,
                Author = "The smiths",
                Category =  category,
                CategoryId = category.Id,
                Duration = 1.2,
                ContentURL = "http://this-charming-man.mp3",
                ImageURL = "",
                Name = "This charming man" 
            };

            Playlist p = new Playlist { 
                Id = 2,
                Category = category,
                CategoryId = category.Id,
                Description = "Best of 80s rock", 
                ImageURL = "", 
                Name = "Alternative rock", 
                Contents = new List<PlayableContent> { auxContent } 
            };

            mock.Setup(x => x.AddPlaylist(p));
            var result = controller.CreatePlaylist(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddExistingPlaylistTest ()
        {

            Playlist p = new Playlist
            {
                Id = 1,
                Category = category,
                CategoryId = category.Id,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { content }
            };

            mock.Setup(x => x.AddPlaylist(p)).Throws(new ArgumentException());
            var result = controller.CreatePlaylist(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
            mock.VerifyAll();

        }

        [TestMethod]
        public void AddContentToPlaylistTest()
        {
            PlayableContent auxContent = new PlayableContent
            {
                Id = 2,
                Author = "Buitres",
                Category = category,
                CategoryId = category.Id,
                Duration = 2.2,
                ContentURL = "http://cadillac-solitario.mp3",
                ImageURL = "",
                Name = "Cadillac solitario"
            };

            mock.Setup(x => x.AddContentToPlaylist(playlist.Id, 2)).Returns(new Playlist
            {
                Id = 1,
                Category = category,
                CategoryId = category.Id,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { content, auxContent }
            });
            var result = controller.AddContentToPlaylist(playlist.Id,2);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddContentToNonExistingPlaylistTest()
        {

            Playlist playlist = new Playlist
            {
                Id = 2,
                Category = category,
                CategoryId = category.Id,
                Description = "Best of 80s rock",
                ImageURL = "",
                Name = "Alternative rock",
                Contents = new List<PlayableContent> {  }
            };

            mock.Setup(x => x.AddContentToPlaylist(2,content.Id)).Returns(new Playlist
            {
                Id = 2,
                Category = category,
                CategoryId = category.Id,
                Description = "Best of 80s rock",
                ImageURL = "",
                Name = "Alternative rock",
                Contents = new List<PlayableContent> { content }
            }
            );
            var result = controller.AddContentToPlaylist(playlist.Id, content.Id);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();

        }

        [TestMethod]
        public void DeletePlaylistByIdTest()
        {
            mock.Setup(x => x.DeletePlaylist(1));
            var result = controller.DeletePlaylistById(1);
            var objectResult = result as NoContentResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(204, statusCode);
            mock.VerifyAll();

        }

        [TestMethod]
        public void DeleteInvalidPlaylistByIdTest()
        {

            mock.Setup(x => x.DeletePlaylist(-1)).Throws(new NullReferenceException());
            var result = controller.DeletePlaylistById(-1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }
    }
}
