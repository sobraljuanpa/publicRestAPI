using System.Collections.Generic;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using WebAPI.Controllers;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTests
{
    [TestClass]
    public class PlayableContentControllerTests
    {

        private Mock<IPlayerBL> mock;
        private IEnumerable<PlayableContent> contents;
        private Category auxCategory;
        private PlayableContent auxPlayableContent;
        PlayableContentController controller;

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

            mock = new Mock<IPlayerBL>(MockBehavior.Strict);
            controller = new PlayableContentController(mock.Object);
        }

        [TestMethod]
        public void GetContentByIdTest()
        {
            mock.Setup(x => x.GetPlayableContent(1)).Returns(auxPlayableContent);

            var result = controller.GetContentById(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetNonExistantContentByIdTest()
        {
            mock.Setup(x => x.GetPlayableContent(0)).Throws(new NullReferenceException());
            
            var result = controller.GetContentById(0);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddContentTest()
        {
            Category c = new Category { Id = 3, Name = "Musica"};
            PlayableContent p = new PlayableContent { Author = "Joy Division", Category = c, ContentURL = "http://disorder.mp3", Duration = 1.2, ImageURL = "", Name = "Disorder" };

            mock.Setup(x => x.AddIndependentContent(p)).Returns(p);
            
            var result = controller.CreateContent(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddRepeatedContentTest()
        {
            Category c = new Category { Id = 3, Name = "Musica" };
            PlayableContent p = new PlayableContent { Author = "Buitres", Category = c, ContentURL = "http://disorder.mp3", Duration = 1.2, ImageURL = "", Name = "Cadillac solitario" };
            mock.Setup(x => x.AddIndependentContent(p)).Throws(new ArgumentException());

            var result = controller.CreateContent(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteContentByIdTest()
        {
            mock.Setup(x => x.DeleteContent(1));
            var result = controller.DeleteContentById(1);
            var objectResult = result as NoContentResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(204, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteInvalidContentByIdTest()
        {
            mock.Setup(x => x.DeleteContent(30)).Throws(new NullReferenceException());
            var result = controller.DeleteContentById(30);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

    }
}
