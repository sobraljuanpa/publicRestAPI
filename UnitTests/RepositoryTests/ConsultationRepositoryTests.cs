using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;
using System;

namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class ConsultationRepositoryTests
    {

        ConsultationRepository repository;

        [TestInitialize]
        public void SetUp()
        {

            var data = new List<Consultation>
            {
                 new Consultation 
                 { 
                     Id = 1, 
                     PatientName = "Juana", 
                     PatientBirthDate = new DateTime(1990, 01, 01), 
                     PatientEmail = "juana@hotmail.com", 
                     PatientPhone = "098765432" 
                 },
                 new Consultation 
                 { 
                     Id = 2, 
                     PatientName = "Victor", 
                     PatientBirthDate = new DateTime(1989,02,02), 
                     PatientEmail = "victor@hotmail.com", 
                     PatientPhone = "02345678" 
                 }
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<Context>()
                 .UseInMemoryDatabase("BetterCalmDB")
                 .Options;

            Context context = new Context(options);

            context.Database.EnsureDeleted();
            context.Set<Consultation>().AddRange(data);
            context.SaveChanges();


            repository = new ConsultationRepository(context);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var consultation = repository.GetAll();

            int actual = consultation.ToList().Count;
            int result = 2;

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            string result = "juana@hotmail.com";

            var consultation = repository.Get(1);

            Assert.AreEqual(result, consultation.PatientEmail);
        }

        [TestMethod]
        public void AddConsultationTest()
        {
            var consultation = new Consultation 
            { 
                Id = 3, 
                PatientName = "Pablo", 
                PatientBirthDate = new DateTime(1996,03,03), 
                PatientEmail = "pablo@hotmail.com", 
                PatientPhone = "098567342" 
            };

            repository.Add(consultation);

            Assert.AreEqual(repository.GetAll().ToList().Count, 3);
        }

        [TestMethod]
        public void UpdateConsultationTest()
        {
            var consultation = new Consultation 
            { 
                Id = 1, 
                PatientName = "Juana", 
                PatientBirthDate = new DateTime(1990, 01, 01), 
                PatientEmail = "juana@gmail.com",
                PatientPhone = "098765432" 
            };
            repository.Update(1, consultation);
            var modifiedConsultation = repository.Get(1);

            
            Assert.AreEqual(consultation.PatientEmail, modifiedConsultation.PatientEmail);
        }

        [TestMethod]
        public void DeleteConsultationTest()
        {
            repository.Delete(1);

            Assert.AreEqual(repository.GetAll().ToList().Count, 1);
        }
    }
}
