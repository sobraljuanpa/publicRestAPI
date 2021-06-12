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
    public class PsychologistRepositoryTests
    {

        PsychologistRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            var expertiseDespression = new Problem 
            { 
                Id = 1, 
                Name = "Depresión"
            };
            var expertiseStress = new Problem 
            { 
                Id = 2, 
                Name = "Estrés"
            };
            var data = new List<Psychologist>
            {
                new Psychologist
                {
                    Id = 1,
                    PsychologistName = "Martin",
                    PsychologistSurname = "Perez",
                    IsRemote = true,
                    Address = "1234567",
                    Expertise = new List<Problem> { expertiseDespression },
                    Schedule = new Schedule()
                },
                new Psychologist 
                { 
                    Id = 2, 
                    PsychologistName = "María",
                    PsychologistSurname = "Lopez", 
                    IsRemote = false, 
                    Address = "", 
                    Expertise = new List<Problem> { expertiseStress },
                    Schedule = new Schedule()
                }
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

            int result = 2;
            int actual = pyschologist.ToList().Count;

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            string result = "Perez";

            var pyschologist = repository.Get(1);

            Assert.AreEqual(result, pyschologist.PsychologistSurname);
        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            var expertiseDespression = new Problem 
            { 
                Name = "Depresión" 
            };
            var expertiseAnger = new Problem 
            { 
                Name = "Enojo" 
            };
            var psychologist = new Psychologist 
            { 
                Id = 3, 
                PsychologistName = "Pedro", 
                PsychologistSurname = "Martinez", 
                IsRemote = true, 
                Address = "1414141", 
                Expertise = new List<Problem> { expertiseDespression, expertiseAnger } 
            };

            repository.Add(psychologist);

            int result = 3;
            int actual = repository.GetAll().Count();

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void UpdatePsychologistTest()
        {

            var expertiseDespression = new Problem 
            {
                Name = "Depresión" 
            };
            var expertiseStress = new Problem 
            { 
                Name = "Estrés" 
            };
            var psychologist = new Psychologist
            {
                Id = 2,
                PsychologistName = "María",
                PsychologistSurname = "Lopez",
                IsRemote = false,
                Address = "",
                Expertise = new List<Problem> { expertiseStress, expertiseDespression },
                Schedule = new Schedule()
            };

            repository.Update(2, psychologist);
            var modifiedPsychologist = repository.Get(2);

            int result = 2;
            int actual = modifiedPsychologist.Expertise.Count;

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void DeletePsychologistTest()
        {
            repository.Delete(1);

            int actual = repository.GetAll().Count();

            Assert.AreEqual(1, actual);
        }


    }
}
