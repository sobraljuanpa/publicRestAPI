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
    public class PlayableContentValidatorTests
    {

        private Mock<IRepository<PlayableContent>> mock;
        private IEnumerable<PlayableContent> contents;
        private PlayableContentValidator validator;

        [TestInitialize]
        public void SetUp()
        {

            mock = new Mock<IRepository<PlayableContent>>(MockBehavior.Strict);
            validator = new PlayableContentValidator(mock.Object);
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
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
        }

        [TestMethod]
        public void IdInValidRangeTest()
        {
            mock.Setup(x => x.GetAll()).Returns(contents.AsQueryable);
            validator.IdInValidRange(2);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IdNotInValidRangeTest()
        {
            mock.Setup(x => x.GetAll()).Returns(contents.AsQueryable);
            validator.IdInValidRange(5);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateRepeatedContentTest()
        {
            mock.Setup(x => x.GetAll()).Returns(contents.AsQueryable);
            PlayableContent newContent = new PlayableContent {
                Author = "Buenos Muchachos",
                Name = "Sin hogar"
            };
            validator.ValidateContent(newContent);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidContentTest()
        {
            mock.Setup(x => x.GetAll()).Returns(contents.AsQueryable);
            PlayableContent newContent = new PlayableContent {
                Id = 1
            };
            validator.ValidateContent(newContent);
            mock.VerifyAll();
        }
    }
}