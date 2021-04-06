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

namespace UnitTests.ControllersTest
{
    [TestClass]

    public class AdministratorControllerTests
    {

        Mock<DbSet<Administrator>> administratorMockSet;
        AdministratorRepository administratorRepository;
        AdministratorBL administratorBL;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;
        AdministratorsController controller;

        [TestInitialize]
        public void SetUp()
        {

            var dataAdministrator = new List<Administrator>
            {
                new Administrator 
                {
                    Id = 1,
                    Email = "admin@admin.admin",
                    Name = "admin",
                    Password = "admin"
                }
            }.AsQueryable();

            administratorMockSet = new Mock<DbSet<Administrator>>();
            administratorMockSet.As<IQueryable<Administrator>>().Setup(m => m.Expression).Returns(dataAdministrator.Expression);
            administratorMockSet.As<IQueryable<Administrator>>().Setup(m => m.ElementType).Returns(dataAdministrator.ElementType);
            administratorMockSet.As<IQueryable<Administrator>>().Setup(m => m.GetEnumerator()).Returns(dataAdministrator.GetEnumerator());
            administratorMockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => dataAdministrator.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);

            mockContext.Setup(v => v.Administrators).Returns(administratorMockSet.Object);
            administratorRepository = new AdministratorRepository(mockContext.Object);

            administratorBL = new AdministratorBL(administratorRepository);
            controller = new AdministratorsController(administratorBL);
        }

        [TestMethod]
        public void GetAdministratorByIdTest ()
        {
            var result = controller.GetAdministratorById(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetNonExistingAdministratorByIdTest()
        {
            var result = controller.GetAdministratorById(0);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public void AuthenticateTest ()
        {
            Administrator admin = new Administrator
            { 
                Id = 1,
                Email = "admin@admin.admin",
                Name = "admin",
                Password = "admin"
            };
            var result = controller.Authenticate(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(202, statusCode);
        }

        [TestMethod]
        public void InvalidAuthenticationTest ()
        {
            Administrator admin = new Administrator
            {
                Id = 1,
                Email = "chiara@admin.admin",
                Name = "chiara",
                Password = "chiara"
            };
            var result = controller.Authenticate(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(401, statusCode);
        }

        [TestMethod]
        public void AddAdministratorTest ()
        {
            Administrator admin = new Administrator
            {
                Id = 1,
                Email = "chiara@chiara.chiara",
                Name = "chiara",
                Password = "chiara"
            };
            var result = controller.AddAdministrator(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
        }

        [TestMethod]
        public void AddExistingAdministratorTest ()
        {
            Administrator admin = new Administrator
            {
                Id = 1,
                Email = "admin@admin.admin",
                Name = "admin",
                Password = "admin"
            };

            var result = controller.AddAdministrator(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
        }

        [TestMethod]
        public void DeleteAdministratorTest ()
        {
            Administrator admin = new Administrator
            {
                Id = 1,
                Email = "chiara@chiara.chiara",
                Name = "chiara",
                Password = "chiara"
            };

            var addAdmin = controller.AddAdministrator(admin);
            var result = controller.DeleteAdministrator(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void DeleteInvalidAdministratorTest ()
        {
           var admin = controller.GetAdministratorById(0);
            var result = controller.DeleteAdministrator(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
        }
    }
}
