using System.Collections.Generic;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using WebAPI.Controllers;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTest
{
    [TestClass]

    public class AdministratorsControllerTests
    {

        private Mock<IAdministratorBL> mock;
        private Administrator admin;
        private IEnumerable<Administrator> administrators;
        private AdministratorsController controller;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IAdministratorBL>(MockBehavior.Strict);
            administrators = new List<Administrator>
            {
                 new Administrator 
                 { 
                     Id = 1, 
                     Email = "chiara@hotmail.com", 
                     Name = "Chiara", 
                     Password= "123chiara987"
                 },
                 new Administrator 
                 { 
                     Id = 2, 
                     Email = "juanPablo@gmail.com", 
                     Name = "Juan Pablo", 
                     Password = "987juan123" 
                 }
            };
            admin = new Administrator 
            { 
                Id = 3, 
                Email = "lorenzo@gmail.com", 
                Name = "Lorenzo", 
                Password = "123lorenzo" 
            };

            controller = new AdministratorsController(mock.Object);
        }

        [TestMethod]
        public void GetAdministratorByIdTest ()
        {
            mock.Setup(x => x.Get(1))
                .Returns(new Administrator 
                { 
                    Id = 1, 
                    Email = "chiara@hotmail.com", 
                    Name = "Chiara", 
                    Password = "123chiara987" 
                });

            var result = controller.GetAdministratorById(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetNonExistingAdministratorByIdTest()
        {
            mock.Setup(x => x.Get(0))
                .Throws(new NullReferenceException());

            var result = controller.GetAdministratorById(0);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
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

            mock.Setup(x => x.Authenticate("admin@admin.admin", "admin"))
                .Returns(admin);

            var result = controller.Authenticate(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(202, statusCode);
            mock.VerifyAll();
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
            Administrator aux = null;

            mock.Setup(x => x.Authenticate("chiara@admin.admin", "chiara"))
                .Returns(aux);

            var result = controller.Authenticate(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(401, statusCode);
            mock.VerifyAll();
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

            mock.Setup(x => x.AddAdministrator(admin));

            var result = controller.AddAdministrator(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
            mock.VerifyAll();
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

            mock.Setup(x => x.AddAdministrator(admin)).
                Throws(new ArgumentException());

            var result = controller.AddAdministrator(admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteAdministratorTest ()
        {
            mock.Setup(x => x.DeleteAdministrator(1));

            var result = controller.DeleteAdministrator(1);
            var objectResult = result as NoContentResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(204, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteInvalidAdministratorTest ()
        {
            mock.Setup(x => x.DeleteAdministrator(0)).
                Throws(new NullReferenceException());

            var result = controller.DeleteAdministrator(0);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateAdministratorTest ()
        {
            Administrator admin = new Administrator
            {
                Id = 2,
                Email = "juan@juan.juan",
                Name = "juan",
                Password = "juan"
            };

            mock.Setup(x => x.UpdateAdministrator(1, admin));

            var result = controller.UpdateAdministrator(1, admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateNonExistingAdministratorTest ()
        {
            Administrator admin = new Administrator
            {
                Id = 2,
                Email = "juan@juan.juan",
                Name = "juan",
                Password = "juan"
            };

            mock.Setup(x => x.UpdateAdministrator(0, admin)).
                Throws(new NullReferenceException());

            var result = controller.UpdateAdministrator(0, admin);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }
    }
}
