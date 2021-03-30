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
    public class ProblemRepositoryTests
    {
        
        ProblemRepository repository;
        Mock<DbSet<Problem>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestMethod]
        public void SetUp()
        {
            var data = new List<Problem>
            {
                new Problem { Id = 1, Name= "Depresión" },
                new Problem { Id = 2, Name= "Estrés" },
                new Problem { Id = 3, Name= "Ansiedad" },
                new Problem { Id = 4, Name= "Autoestima" },
                new Problem { Id = 5, Name= "Enojo" },
                new Problem { Id = 6, Name= "Relaciones" },
                new Problem { Id = 7, Name= "Duelo" },
                new Problem { Id = 8, Name= "Y más" }

            }.AsQueryable();

            mockSet = new Mock<DbSet<Problem>>();
            mockSet.As<IQueryable<Problem>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Problem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Problem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.Problems).Returns(mockSet.Object);

            repository = new ProblemRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var problems = repository.GetAll();

            Assert.AreEqual(8, problems.ToList().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var problems = repository.Get(1);

            Assert.AreEqual("Depresión", problems.Name);
        }

        [TestMethod]
        public void AddProblemTest()
        {
            var problem = new Problem { Id = 9, Name = "Familiar" };
            repository.Add(problem);

            mockSet.Verify(v => v.Add(It.IsAny<Problem>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateProblemTest()
        {
            var problem = new Problem { Id = 1, Name = "Discapacidad" };
            repository.Update(1, problem);
            var modifiedProblem = repository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("Discapacidad", modifiedProblem.Name);
        }

        [TestMethod]
        public void DeleteProblemTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }
    }
}
