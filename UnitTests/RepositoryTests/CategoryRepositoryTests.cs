using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;

namespace UnitTests
{
    [TestClass]
    public class CategoryRepositoryTests
    {

        CategoryRepository repository;
        Mock<DbSet<Category>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

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

            mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.Categories).Returns(mockSet.Object);

            repository = new CategoryRepository(mockContext.Object);
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

            mockSet.Verify(v => v.Add(It.IsAny<Category>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var category = new Category { Id = 1, Name = "Bailar" };
            repository.Update(1, category);
            var modifiedCategory = repository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("Bailar", modifiedCategory.Name);
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual(3, repository.GetAll().ToList().Count);
        }
    }
}
