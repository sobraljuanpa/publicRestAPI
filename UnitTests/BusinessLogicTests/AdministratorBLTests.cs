using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using IDataAccess;
using BusinessLogic;
using System;

namespace UnitTests.BusinessLogicTests
{
    [TestClass]
    public class AdministratorBLTests
    {
        private Mock<IAdministratorRepository<Administrator>> mock;
        private AdministratorBL businessLogic;
        private Administrator admin;
        private IEnumerable<Administrator> administrators;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            businessLogic = new AdministratorBL(mock.Object);
            administrators = new List<Administrator>
            {
                 new Administrator { Id = 1, Email = "chiara@hotmail.com", Name = "Chiara", Password= "123chiara987"},
                 new Administrator { Id = 2, Email = "juanPablo@gmail.com", Name = "Juan Pablo", Password = "987juan123" }
            };

            admin = new Administrator { Id = 3, Email = "lorenzo@gmail.com", Name = "Lorenzo", Password = "123lorenzo" };
        }

        [TestMethod]
        public void AuthenticateValidCredentialsTest()
        {
            mock.Setup(x => x.Authenticate("chiara@hotmail.com", "123chiara987"))
                .Returns(new Administrator { Id = 1, Email = "chiara@hotmail.com", Name = "Chiara", Password = null });
            businessLogic.Authenticate("chiara@hotmail.com", "123chiara987");
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddAdministratorTest()
        {
            mock.Setup(x => x.GetAll()).Returns(administrators.AsQueryable);
            mock.Setup(x => x.Add(admin));
            businessLogic.AddAdministrator(admin);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteAdministratorTest()
        {
            mock.Setup(x => x.GetAll()).Returns(administrators.AsQueryable);
            mock.Setup(x => x.Delete(1));
            businessLogic.DeleteAdministrator(1);
            mock.VerifyAll();
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void DeleteOutOfRangeAdministratorTest()
        {
            mock.Setup(x => x.GetAll()).Returns(administrators.AsQueryable);
            businessLogic.DeleteAdministrator(10);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateAdministratorTest()
        {
            mock.Setup(x => x.Update(1, admin));
            businessLogic.UpdateAdministrator(1, admin);
            mock.VerifyAll();
        }

        
    }
}
