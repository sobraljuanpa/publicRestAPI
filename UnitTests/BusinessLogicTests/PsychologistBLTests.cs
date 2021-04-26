using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;
using BusinessLogic;
using System;

namespace UnitTests.BusinessLogicTests
{
    [TestClass]
    public class PsychologistBLTests
    {
        private Mock<IRepository<Psychologist>> mock;
        private PsychologistBL businessLogic;
        private IEnumerable<Psychologist> data;
        private Problem expertiseStress;
        private Problem expertiseDepression;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IRepository<Psychologist>>(MockBehavior.Strict);
            businessLogic = new PsychologistBL(mock.Object);

            expertiseDepression = new Problem { Id = 1, Name = "Depresión" };
            expertiseStress = new Problem { Id = 2, Name = "Estrés" };
            data = new List<Psychologist>
            {
                new Psychologist { Id = 1, PsychologistName = "Martin", PsychologistSurname = "Perez", IsRemote = true, Address = "1234567", Expertise = new List<Problem> { expertiseDepression } },
                new Psychologist { Id = 2, PsychologistName = "María", PsychologistSurname = "Lopez", IsRemote = false, Address = "", Expertise = new List<Problem> { expertiseStress } }
            }.AsQueryable();
            
        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0}
            };

            mock.Setup(x => x.Add(p));
            mock.Setup(x => x.Get(p.Id)).Returns(p);
            
            businessLogic.AddPsychologist(p);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeletePsychologistTest()
        {
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mock.Setup(x => x.Delete(1));
            businessLogic.DeletePsychologist(1);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeletePsychologistInvalidIdTest()
        {
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            businessLogic.DeletePsychologist(30);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetPsychologistTest()
        {
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mock.Setup(x => x.Get(1)).Returns(new Psychologist { Id = 1, PsychologistName = "Martin", PsychologistSurname = "Perez", IsRemote = true, Address = "1234567", Expertise = new List<Problem> { expertiseDepression } });
            
            var aux = businessLogic.GetPsychologist(1);
            Assert.AreEqual("Martin", aux.PsychologistName);
            
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetPsychologistInvalidIdTest()
        {
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);

            businessLogic.GetPsychologist(10);

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetPsychologistsTest()
        {
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);

            var aux = businessLogic.GetPsychologists();
            
            Assert.AreEqual(2, aux.Count);

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecialistsTest()
        {
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);

            var aux = businessLogic.GetSpecialists(expertiseDepression);

            Assert.AreEqual(1, aux.Count);

            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdatePsychologistTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
            };
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mock.Setup(x => x.Update(1, p));
            businessLogic.UpdatePsychologist(1, p);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdatePsychologistInvalidIdTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
            };
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mock.Setup(x => x.Update(10, p));
            businessLogic.UpdatePsychologist(10, p);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateScheduleTest()
        {
            var psy = new Psychologist { Id = 1, PsychologistName = "Martin", PsychologistSurname = "Perez", IsRemote = true, Address = "1234567", Expertise = new List<Problem> { expertiseDepression } };
            var schedule =  new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 };
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mock.Setup(x => x.Get(1)).Returns(psy);
            psy.Schedule = schedule;
            mock.Setup(x => x.Update(1, psy));
            businessLogic.UpdateSchedule(1, schedule);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateScheduleInvalidIdTest()
        {
            var psy = new Psychologist { Id = 1, PsychologistName = "Martin", PsychologistSurname = "Perez", IsRemote = true, Address = "1234567", Expertise = new List<Problem> { expertiseDepression } };
            var schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 };
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            psy.Schedule = schedule;
            mock.Setup(x => x.Update(10, psy));
            businessLogic.UpdateSchedule(10, schedule);
            mock.VerifyAll();
        }

    }
}
