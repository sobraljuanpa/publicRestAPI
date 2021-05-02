﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using IDataAccess;
using BusinessLogic;

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

            expertiseDepression = new Problem 
            { 
                Id = 1, 
                Name = "Depresión" 
            };
            expertiseStress = new Problem 
            { 
                Id = 2, 
                Name = "Estrés" 
            };
            data = new List<Psychologist>
            {
                new Psychologist 
                { 
                    Id = 1, 
                    PsychologistName = "Martin", 
                    PsychologistSurname = "Perez", 
                    IsRemote = true, 
                    Address = "1234567", 
                    Expertise = new List<Problem> { expertiseDepression } 
                },
                new Psychologist 
                { 
                    Id = 2, 
                    PsychologistName = "María", 
                    PsychologistSurname = "Lopez", 
                    IsRemote = false, Address = "", 
                    Expertise = new List<Problem> { expertiseStress } 
                }
            }.AsQueryable();

            
        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            Psychologist psychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule 
                { 
                    MondayConsultations = 0, 
                    TuesdayConsultations = 0, 
                    WednesdayConsultations = 0, 
                    ThursdayConsultations = 0, 
                    FridayConsultations = 0
                }
            };

            mock.Setup(x => x.Add(psychologist));
            mock.Setup(x => x.Get(psychologist.Id)).Returns(psychologist);
            
            businessLogic.AddPsychologist(psychologist);

            mock.VerifyAll();
        }

        [TestMethod]
        public void DeletePsychologistTest()
        {
            mock.Setup(x => x.Delete(1));

            businessLogic.DeletePsychologist(1);

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetPsychologistTest()
        {
            mock.Setup(x => x.Get(1)).Returns(new Psychologist 
                                             { 
                                                Id = 1, 
                                                PsychologistName = "Martin",
                                                PsychologistSurname = "Perez", 
                                                IsRemote = true, 
                                                Address = "1234567", 
                                                Expertise = new List<Problem> { expertiseDepression } 
                                             });
            
            var aux = businessLogic.GetPsychologist(1);

            Assert.AreEqual("Martin", aux.PsychologistName);
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
            Psychologist psychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule 
                { 
                    MondayConsultations = 0, 
                    TuesdayConsultations = 0, 
                    WednesdayConsultations = 0, 
                    ThursdayConsultations = 0, 
                    FridayConsultations = 0 
                }
            };
            mock.Setup(x => x.Update(1, psychologist));

            businessLogic.UpdatePsychologist(1, psychologist);

            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateScheduleTest()
        {
            var psychologist = new Psychologist 
            { 
                Id = 1, 
                PsychologistName = "Martin", 
                PsychologistSurname = "Perez", 
                IsRemote = true,
                Address = "1234567", 
                Expertise = new List<Problem> { expertiseDepression } 
            };
            var schedule =  new Schedule 
            { 
                MondayConsultations = 0, 
                TuesdayConsultations = 0, 
                WednesdayConsultations = 0, 
                ThursdayConsultations = 0, 
                FridayConsultations = 0
            };

            mock.Setup(x => x.Get(1)).Returns(psychologist);
            psychologist.Schedule = schedule;
            mock.Setup(x => x.Update(1, psychologist));

            businessLogic.UpdateSchedule(1, schedule);

            mock.VerifyAll();
        }

         

}
}
