using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using Domain.DTOs;
using IDataAccess;
using BusinessLogic;
using System;

namespace UnitTests.BusinessLogicTests
{
    [TestClass]
    public class PsychologistBLTests
    {
        private Mock<IRepository<Psychologist>> mock;
        private Mock<IRepository<Problem>> mockProblem;
        private Mock<IRepository<Schedule>> mockSchedule;
        private PsychologistBL businessLogic;
        private IEnumerable<Psychologist> data;
        private Problem expertiseStress;
        private Problem expertiseDepression;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IRepository<Psychologist>>(MockBehavior.Strict);
            mockProblem = new Mock<IRepository<Problem>>(MockBehavior.Strict);
            mockSchedule = new Mock<IRepository<Schedule>>(MockBehavior.Strict);
            businessLogic = new PsychologistBL(mock.Object, mockProblem.Object, mockSchedule.Object);

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
                    Fee= 1000,
                    Expertise = new List<Problem> { expertiseDepression } 
                },
                new Psychologist 
                { 
                    Id = 2, 
                    PsychologistName = "María", 
                    PsychologistSurname = "Lopez", 
                    IsRemote = false, 
                    Address = "", 
                    Fee = 2000,
                    Expertise = new List<Problem> { expertiseStress } 
                }
            }.AsQueryable();
            
        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            PsychologistDTO psychologist = new PsychologistDTO
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                IsRemote = false,
                Fee = 500
            };

            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockSchedule.Setup(x => x.GetAll()).Returns(new List<Schedule> { }.AsQueryable);
            mockSchedule.Setup(x => x.Add(It.IsAny<Schedule>()));
            mockSchedule.Setup(x => x.Get(0)).Returns(new Schedule());

            mock.Setup(x => x.Add(It.IsAny<Psychologist>()));
            mock.Setup(x => x.Get(psychologist.Id)).Returns(businessLogic.ToEntity(psychologist));

            businessLogic.AddPsychologist(psychologist);

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddPsychologistWithInvalidFeeTest()
        {
            PsychologistDTO psychologist = new PsychologistDTO
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                IsRemote = false,
                Fee = 100
            };

            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockSchedule.Setup(x => x.GetAll()).Returns(new List<Schedule> { }.AsQueryable);
            mockSchedule.Setup(x => x.Add(It.IsAny<Schedule>()));
            mockSchedule.Setup(x => x.Get(0)).Returns(new Schedule());

            mock.Setup(x => x.Add(It.IsAny<Psychologist>()));
            mock.Setup(x => x.Get(psychologist.Id)).Returns(businessLogic.ToEntity(psychologist));

            businessLogic.AddPsychologist(psychologist);

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
            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable());
            mock.Setup(x => x.Get(1)).Returns(new Psychologist 
            { 
                Id = 1, 
                PsychologistName = "Martin",
                PsychologistSurname = "Perez", 
                IsRemote = true, 
                Address = "1234567", 
                Expertise = new List<Problem> 
                { 
                    expertiseDepression 
                }
            });

            
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
            PsychologistDTO psychologist = new PsychologistDTO
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                IsRemote = false,
                Fee= 1000
            };

            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable());
            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem());
            mock.Setup(x => x.Update(1, It.IsAny<Psychologist>()));

            businessLogic.UpdatePsychologist(1, psychologist);

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdatePsychologistInvalidIdTest()
        {
            PsychologistDTO p = new PsychologistDTO
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                IsRemote = false
            };

            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem());
            mock.Setup(x => x.Update(10, businessLogic.ToEntity(p)));
            
            businessLogic.UpdatePsychologist(10, p);
            
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdatePsychologistInvalidFeeTest()
        {
            PsychologistDTO psychologist = new PsychologistDTO
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                IsRemote = false,
                Fee= 11
            };

            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable);
            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem());
            mock.Setup(x => x.Update(10, businessLogic.ToEntity(psychologist)));

            businessLogic.UpdatePsychologist(10, psychologist);

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

            mock.Setup(x => x.GetAll()).Returns(data.AsQueryable());
            psychologist.Schedule = schedule;
            mockSchedule.Setup(x => x.Get(1)).Returns(new Schedule());
            mockSchedule.Setup(x => x.Update(1, schedule));
            businessLogic.UpdateSchedule(1, schedule);

            mock.VerifyAll();
        }

        [TestMethod]
        public void AddProblemToPsychologistTest()
        {
            var psychologist = new Psychologist
            {
                Id = 1,
                PsychologistName = "Martin",
                PsychologistSurname = "Perez",
                IsRemote = false,
                Address = "1234567",
                Expertise = new List<Problem> { expertiseDepression }
            };

            mockProblem.Setup(x => x.Get(2)).Returns(expertiseStress);
            mock.Setup(x => x.Update(1, psychologist));

            businessLogic.AddProblemToPsychologist(psychologist, expertiseStress.Id);

            mock.VerifyAll();
        }



        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddAlreadyExistingProblemToPsychologistTest()
        {
            var psychologist = new Psychologist
            {
                Id = 1,
                PsychologistName = "Martin",
                PsychologistSurname = "Perez",
                IsRemote = false,
                Address = "1234567",
                Expertise = new List<Problem> { expertiseDepression }
            };

            mockProblem.Setup(x => x.Get(1)).Returns(expertiseDepression);
            mock.Setup(x => x.Update(1, psychologist));

            businessLogic.AddProblemToPsychologist(psychologist, expertiseDepression.Id);

            mock.VerifyAll();
        }
    }
}
