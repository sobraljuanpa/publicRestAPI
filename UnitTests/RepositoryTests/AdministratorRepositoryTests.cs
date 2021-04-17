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

        [TestInitialize]
        public void SetUp()
        {
            var admin = new Administrator 
            { 
                Id = 1, 
                Email = "chiara@hotmail.com", 
                Name = "Chiara", 
                Password = "123chiara987" 
            };

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("BetterCalmDB")
                .Options;

            Context context = new Context(options);

            context.Database.EnsureDeleted();
            context.Set<Administrator>().Add(admin);
            context.SaveChanges();

            repository = new AdministratorRepository(context);
        }

        [TestMethod]
        public void AuthenticateTest()
        {
            Assert.IsNotNull(repository.Authenticate("chiara@hotmail.com", "123chiara987"));
        }

        [TestMethod]
        public void GetAllTest()
        {
            var administrators = repository.GetAll();

            Assert.AreEqual(1, administrators.ToList().Count);
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
            
            var count = repository.GetAll().Count();
            
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void UpdateAdministratorTest()
        {
            var administrator = new Administrator { Id = 1, Email = "chiara@hotmail.com", Name = "Chiara", Password = "chiara123987" };
            repository.Update(1, administrator);
            var modifiedAdministrator = repository.Get(1);
            Assert.AreEqual("chiara123987", modifiedAdministrator.Password);
        }

        [TestMethod]
        public void DeleteAdministratorTest()
        {
            repository.Delete(1);

            var count = repository.GetAll().Count();

            Assert.AreEqual(0, count);
        }
    }
}
