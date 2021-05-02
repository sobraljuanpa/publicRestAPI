using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

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

            int actual = categories.ToList().Count;
            int result = 4;

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            string result = "Dormir";

            var category = repository.Get(1);

            Assert.AreEqual(result, category.Name);
        }

        [TestMethod]
        public void AddCategoryTest()
        {
            var category = new Category { Id = 5, Name = "Aprender" };
            int result = category.Id;
            
            repository.Add(category);
            
            int actual = repository.GetAll().Count();

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var category = new Category { Id = 1, Name = "Bailar" };
            string result = category.Name;

            repository.Update(1, category);

            var modifiedCategory = repository.Get(1);

            Assert.AreEqual(result, modifiedCategory.Name);
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            repository.Delete(1);

            int actual = repository.GetAll().Count();
            int result = 3;

            Assert.AreEqual(result, actual);
        }
    }
}
