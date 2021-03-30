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
    public class PsychologistRepositoryTests
    {

        PsychologistRepository repository;
        Mock<DbSet<Psychologist>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var expertiseDespression = new Problem { Id = 1, Name = "Depresión"};
            var expertiseStress = new Problem { Id = 2, Name = "Estrés"};
            var data = new List<Psychologist>
            {
                new Psychologist { Id = 1, PsychologistName = "Martin", PsychologistSurname = "Perez", IsRemote = true, Address = "1234567", Expertise = new List<Problem> { expertiseDespression } },
                new Psychologist { Id = 2, PsychologistName = "María", PsychologistSurname = "Lopez", IsRemote = false, Address = "", Expertise = new List<Problem> { expertiseStress } }
            }.AsQueryable();

            mockSet = new Mock<DbSet<Psychologist>>();
            mockSet.As<IQueryable<Psychologist>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Psychologist>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Psychologist>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.Psychologists).Returns(mockSet.Object);

            repository = new PsychologistRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var pyschologist = repository.GetAll();

            Assert.AreEqual(2, pyschologist.ToList().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var pyschologist = repository.Get(1);

            Assert.AreEqual("Perez", pyschologist.PsychologistSurname);
        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            var expertiseDespression = new Problem { Id = 1, Name = "Depresión" };
            var expertiseAnger = new Problem { Id = 5, Name = "Enojo" };
            var psychologist = new Psychologist { Id = 3, PsychologistName = "Pedro", PsychologistSurname = "Martinez", IsRemote = true, Address = "1414141", Expertise = new List<Problem> { expertiseDespression, expertiseAnger } };
            repository.Add(psychologist);

            mockSet.Verify(v => v.Add(It.IsAny<Psychologist>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdatePsychologistTest()
        {

            var expertiseDespression = new Problem { Id = 1, Name = "Depresión" };
            var expertiseStress = new Problem { Id = 2, Name = "Estrés" };
            var psychologist = new Psychologist { Id = 2, PsychologistName = "María", PsychologistSurname = "Lopez", IsRemote = false, Address = "", Expertise = new List<Problem> { expertiseStress, expertiseDespression } };
            repository.Update(2, psychologist);
            var modifiedPsychologist = repository.Get(2);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual(2, modifiedPsychologist.Expertise.Count);
        }

        [TestMethod]
        public void DeletePsychologistTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }


    }
}
