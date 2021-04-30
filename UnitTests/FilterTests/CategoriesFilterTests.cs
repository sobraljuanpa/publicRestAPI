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

namespace UnitTests.FilterTests
{
   [TestClass]
    class CategoriesFilterTests
    {
        private Mock<IPlayerBL> mock;
        private Category category;
        private PlayableContent content;
        private Playlist playlist;
        private IEnumerable<Category> categories;
        private IEnumerable<PlayableContent> contents;
        private IEnumerable<Playlist> playlists;
        private IEnumerable<Object> elements;
        private CategoriesController controller;

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
                Id = 2,
                Author = "Jamiroquai",
                Category = category,
                Duration = 4.2,
                ContentURL = "http://La-octava-de-octavio.mp3",
                ImageURL = "",
                Name = "Alright"
            };

            playlist = new Playlist
            {
                Id = 1,
                Category = category,
                Description = "",
                ImageURL = "",
                Name = "Trevelling without moving",
                Contents = new List<PlayableContent> { content }
            };

            contents = new List<PlayableContent>
            {
                new PlayableContent
                {
                    Id = 1,
                    Author = "Jamiroquai",
                    Category = category,
                    Duration = 5.4,
                    ContentURL = "http://aguitaecoco.mp3",
                    ImageURL = "",
                    Name = "Virtual Insanity"
                },
                new PlayableContent
                {
                    Id = 2,
                    Author = "Jamiroquai",
                    Category = category,
                    Duration = 4.2,
                    ContentURL = "http://La-octava-de-octavio.mp3",
                    ImageURL = "",
                    Name = "Alright"
                }
            }.AsQueryable();

            playlists = new List<Playlist>
            {
                new Playlist
                {
                    Id = 1,
                    Category = category,
                    Description = "",
                    ImageURL = "",
                    Name = "Trevelling without moving",
                    Contents = new List<PlayableContent> { content }
                }
            }.AsQueryable();

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

            elements = contents;


            controller = new CategoriesController(mock.Object);
        }

        [TestMethod]

        public void GetCategoryContentsByInvalidIdTest()
        {
            mock.Setup(x => x.GetCategoryElements(0)).Throws(new NullReferenceException());

            var result = controller.GetCategoryContents(0);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

    }
}
