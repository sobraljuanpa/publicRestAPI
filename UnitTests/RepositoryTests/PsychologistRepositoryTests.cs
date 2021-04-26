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

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("BetterCalmDB")
                .Options;

            Context context = new Context(options);

            context.Database.EnsureDeleted();
            context.Set<Psychologist>().AddRange(data);
            context.SaveChanges();

            repository = new PsychologistRepository(context);
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
            var expertiseDespression = new Problem { Name = "Depresión" };
            var expertiseAnger = new Problem { Name = "Enojo" };
            var psychologist = new Psychologist { Id = 3, PsychologistName = "Pedro", PsychologistSurname = "Martinez", IsRemote = true, Address = "1414141", Expertise = new List<Problem> { expertiseDespression, expertiseAnger } };
            repository.Add(psychologist);
            Assert.AreEqual(3, repository.GetAll().Count());
        }

        [TestMethod]
        public void UpdatePsychologistTest()
        {

            var expertiseDespression = new Problem {Name = "Depresión" };
            var expertiseStress = new Problem { Name = "Estrés" };
            var psychologist = new Psychologist { Id = 2, PsychologistName = "María", PsychologistSurname = "Lopez", IsRemote = false, Address = "", Expertise = new List<Problem> { expertiseStress, expertiseDespression } };
            repository.Update(2, psychologist);
            var modifiedPsychologist = repository.Get(2);
            Assert.AreEqual(2, modifiedPsychologist.Expertise.Count);
        }

        [TestMethod]
        public void DeletePsychologistTest()
        {
            repository.Delete(1);

            Assert.AreEqual(1, repository.GetAll().Count());
        }


    }
}
