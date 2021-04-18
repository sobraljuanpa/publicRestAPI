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
    public class CategoryRepositoryTests
    {

        CategoryRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            var data = new List<Category>
            {
                new Category {Id = 1, Name = "Dormir"},
                new Category {Id = 2, Name = "Meditar"},
                new Category {Id = 3, Name = "Musica"},
                new Category {Id = 4, Name = "Cuerpo"},
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("BetterCalmDB")
                .Options;

            Context context = new Context(options);

            context.Database.EnsureDeleted();
            context.Set<Category>().AddRange(data);
            context.SaveChanges();

            repository = new CategoryRepository(context);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var categories = repository.GetAll();

            Assert.AreEqual(4, categories.ToList().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var category = repository.Get(1);

            Assert.AreEqual("Dormir", category.Name);
        }

        [TestMethod]
        public void AddCategoryTest()
        {
            var category = new Category { Id = 5, Name = "Aprender" };
            
            repository.Add(category);
            
            var count = repository.GetAll().Count();

            Assert.AreEqual(5, count);
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var category = new Category { Id = 1, Name = "Bailar" };

            repository.Update(1, category);

            var modifiedCategory = repository.Get(1);

            Assert.AreEqual("Bailar", modifiedCategory.Name);
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            repository.Delete(1);

            var count = repository.GetAll().Count();

            Assert.AreEqual(3, count);
        }
    }
}
