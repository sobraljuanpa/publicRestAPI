using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

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
            string email = "chiara@hotmail.com";
            string password = "123chiara987";

            Assert.IsNotNull(repository.Authenticate(email, password));
        }

        [TestMethod]
        public void GetAllTest()
        {
            var administrators = repository.GetAll();
            int actual = administrators.ToList().Count;

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            string result = "chiara@hotmail.com";

            var administrator = repository.Get(1);

            Assert.AreEqual(result, administrator.Email);
        }

        [TestMethod]
        public void AddAdministratorTest()
        {
            var administrator = new Administrator 
            { 
                Id = 3, 
                Email = "lorenzo@gmail.com", 
                Name = "Lorenzo", 
                Password = "123lorenzo" 
            };
            
            repository.Add(administrator);
            
            var actual = repository.GetAll().Count();
            int result = 2;
            
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void UpdateAdministratorTest()
        {
            var administrator = new Administrator 
            { 
                Id = 1, 
                Email = "chiara@hotmail.com", 
                Name = "Chiara", 
                Password = "chiara123987" 
            };

            repository.Update(1, administrator);
            var modifiedAdministrator = repository.Get(1);

            Assert.AreEqual(administrator.Password, modifiedAdministrator.Password);
        }

        [TestMethod]
        public void DeleteAdministratorTest()
        {
            repository.Delete(1);

            var actual = repository.GetAll().Count();

            Assert.AreEqual(0, actual);
        }
    }
}
