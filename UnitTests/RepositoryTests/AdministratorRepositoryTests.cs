using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;

namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class AdministratorRepositoryTests
    {
        AdministratorRepository repository;
        Mock<DbSet<Administrator>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
         
            var data = new List<Administrator>
            {
                 new Administrator { Id = 1, Email = "chiara@hotmail.com", Name = "Chiara", Password= "123chiara987"},
                 new Administrator { Id = 2, Email = "juanPablo@gmail.com", Name = "Juan Pablo", Password = "987juan123" }
            }.AsQueryable();

            mockSet = new Mock<DbSet<Administrator>>();
            mockSet.As<IQueryable<Administrator>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Administrator>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Administrator>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.Administrators).Returns(mockSet.Object);

            repository = new AdministratorRepository(mockContext.Object);
        }

        [TestMethod]
        public void AuthenticateTest()
        {
            Assert.IsNotNull(repository.Authenticate("chiara@hotmail.com", "123chiara987"));
        }

        [TestMethod]
        public void GetAllTest()
        {
            var administrator = repository.GetAll();

            Assert.AreEqual(2, administrator.ToList().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var administrator = repository.Get(1);

            Assert.AreEqual("chiara@hotmail.com", administrator.Email);
        }

        [TestMethod]
        public void AddAdministratorTest()
        {
            var administrator = new Administrator { Id = 3, Email = "lorenzo@gmail.com", Name = "Lorenzo", Password = "123lorenzo" };
            repository.Add(administrator);

            mockSet.Verify(v => v.Add(It.IsAny<Administrator>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateAdministratorTest()
        {
            var administrator = new Administrator { Id = 1, Email = "chiara@hotmail.com", Name = "Chiara", Password = "chiara123987" };
            repository.Update(1, administrator);
            var modifiedAdministrator = repository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("chiara123987", modifiedAdministrator.Password);
        }

        [TestMethod]
        public void DeleteAdministratorTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }
    }
}
