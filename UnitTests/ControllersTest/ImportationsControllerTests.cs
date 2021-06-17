using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using Domain.DTOs;
using WebAPI.Controllers;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UnitTests.ControllersTest
{
    [TestClass]
    public class ImportationsControllerTests
    {
        private Mock<IImportationBL> mock;
        private ImportationsController controller;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IImportationBL>(MockBehavior.Strict);
            controller = new ImportationsController(mock.Object);
        }
        [TestMethod]
        public void CreateConsultationsTest()
        {
            object[] parameters= new object[5];
            parameters[0] = "path1";

            mock.Setup(x => x.LoadFile("JSON", parameters));

            var result = controller.ImportContent("JSON",parameters);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateInvalidConsultationsTest()
        {

            mock.Setup(x => x.LoadFile("", null));

            var result = controller.ImportContent("", null);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(409, statusCode);
            mock.VerifyAll();
        }
    }
}
