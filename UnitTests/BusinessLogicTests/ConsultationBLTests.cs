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
    public class ConsultationBLTests
    {
        private Mock<IRepository<Consultation>> mockConsultation;
        private Mock<IRepository<Psychologist>> mockPsychologist;
        private Mock<IRepository<Problem>> mockProblem;
        private ConsultationBL businessLogic;
        private Psychologist psychologist;
        private Consultation consultation;
        private Problem problem;
        private Schedule schedule;
        private IEnumerable<Consultation> consultations;
        private IEnumerable<Psychologist> psychologists;

        [TestInitialize]
        public void SetUp()
        {

            problem = new Problem
            {
                Id = 1,
                Name = "Depresión",
            };
            var anotherProblem = new Problem
            {
                Id = 2,
                Name = "Estrés"
            };
            schedule = new Schedule
            {
                MondayConsultations = 5,
                TuesdayConsultations = 0,
                WednesdayConsultations = 0,
                ThursdayConsultations = 0,
                FridayConsultations = 1
            };

            var psychologistExperties = new List<Problem>
            {
                problem,
                anotherProblem
            };
             psychologist = new Psychologist
            {
                Id = 1,
                PsychologistName = "",
                PsychologistSurname = "",
                ActiveYears = 4,
                Schedule = schedule,
                Expertise = psychologistExperties

            };

            consultation = new Consultation
            {
                Id = 2,
                PatientName = "Matias",
                PatientBirthDate = new DateTime(1990, 01, 01),
                PatientEmail = "matias@hotmial.com",
                PatientPhone = "098000000",
                Problem = problem,
                Psychologist = psychologist,
                Address = "",
                IsRemote = false,
                Date = 2,
                Duration = 1,
                Bonus = 50

            };

            consultations = new List<Consultation>
            {
                new Consultation 
                { 
                    Id = 1,
                    PatientName = "Sofia", 
                    PatientBirthDate = new DateTime(1998, 01, 01), 
                    PatientEmail = "sofia@hotmial.com", 
                    PatientPhone = "098999999", 
                    Problem = problem, 
                    Psychologist = psychologist , 
                    Address = "", 
                    IsRemote = true ,
                    Date = 1,
                    Duration = 2,
                    Bonus = 25
                },
                consultation
            }.AsQueryable();

            psychologists = new List<Psychologist>
            {
                psychologist,
                new Psychologist
                {
                    Id = 2,
                    PsychologistName = "",
                    PsychologistSurname = "",
                    ActiveYears = 3,
                    Schedule = schedule,
                    Expertise = psychologistExperties
                }

            }.AsQueryable();

            mockConsultation = new Mock<IRepository<Consultation>>(MockBehavior.Strict);
            mockPsychologist = new Mock<IRepository<Psychologist>>(MockBehavior.Strict);
            mockProblem = new Mock<IRepository<Problem>>(MockBehavior.Strict);

            businessLogic = new ConsultationBL(mockConsultation.Object, mockPsychologist.Object,
                                                mockProblem.Object);
        }

        [TestMethod]
        public void GetConsultationsTest ()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());

            List<Consultation> auxConsultations = businessLogic.GetConsultationsByPsychologist(1);

            Assert.AreEqual(2, consultations.ToList().Count);
            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetInvalidConsultationsTest ()
        {
            mockConsultation.Setup(x => x.GetAll()).Throws(new Exception());

            List<Consultation> auxConsultations = businessLogic.GetConsultationsByPsychologist(-1);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void GetConsultationByIdTest ()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable);
            mockConsultation.Setup(x => x.Get(2)).Returns(consultation);

            ConsultationDTO auxConsultations = businessLogic.Get(2);

            Assert.AreEqual(2, auxConsultations.Id);
            mockConsultation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetInvalidConsultationByIdTest ()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable);
            mockConsultation.Setup(x => x.Get(-1)).Throws(new Exception());

            ConsultationDTO auxConsultations = businessLogic.Get(-1);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void CreateValidConsultationTest ()
        {
            var guid = Guid.NewGuid();
            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address =  "https://betterCalm.com.uy/meeting_id/" + guid.ToString(),
                IsRemote = true,
                Date = 3,
                Duration = 1,
                Bonus = 50
            };


            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(1)).Returns(psychologist);
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            mockPsychologist.Setup(x => x.Update(1, It.IsAny<Psychologist>()));

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));
            mockConsultation.Setup(x => x.Get(newConsultation.Id)).Returns(businessLogic.ToEntity(newConsultation));

            businessLogic.CreateConsultation(newConsultation);

            mockProblem.VerifyAll();
            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateConsultationFullScheduleTest()
        {
            var guid = Guid.NewGuid();

            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address = "https://betterCalm.com.uy/meeting_id/" + guid.ToString(),
                IsRemote = true,
                Date = 0,
                Duration = 1,
                Bonus = 50
            };

            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(It.IsAny<int>())).Returns(new Psychologist { });
            mockPsychologist.Setup(x => x.GetAll()).Returns(new List<Psychologist> { }.AsQueryable);

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));

            businessLogic.CreateConsultation(newConsultation);

            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateConsultationInvalidScheduleTest()
        {
            psychologist.Schedule = null;

            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address = "",
                IsRemote = false,
                Date = 2,
                Duration = 1,
                Bonus = 50
            };


            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(It.IsAny<int>())).Returns(new Psychologist { });
            mockPsychologist.Setup(x => x.GetAll()).Returns(new List<Psychologist> { }.AsQueryable);

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));

            businessLogic.CreateConsultation(newConsultation);

            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidFormatAddressTest()
        {
            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address = "abc.de",
                IsRemote = true,
                Date = 2,
                Duration = 1,
                Bonus = 50
            };


            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(It.IsAny<int>())).Returns(new Psychologist { });
            mockPsychologist.Setup(x => x.GetAll()).Returns(new List<Psychologist> { }.AsQueryable);

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));

            businessLogic.CreateConsultation(newConsultation);

            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        public void MoreThanOnePsychologistTest()
        {
            var guid = Guid.NewGuid();

            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address = "https://betterCalm.com.uy/meeting_id/" + guid.ToString(),
                IsRemote = true,
                Date = 1,
                Duration = 1,
                Bonus = 50
            };

            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(1)).Returns(psychologist);
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            mockPsychologist.Setup(x => x.Update(1, It.IsAny<Psychologist>()));

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));
            mockConsultation.Setup(x => x.Get(newConsultation.Id)).Returns(businessLogic.ToEntity(newConsultation));
            Consultation auxConsultation = businessLogic.CreateConsultation(newConsultation);

            Assert.AreEqual(1, auxConsultation.Psychologist.Id);
            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        public void OnePsychologistForConsultationTest ()
        {
            var guid = Guid.NewGuid();

            var auxProblem = new Problem
            {
                Id = 2,
                Name = "Estrés"
            };
            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmail.com",
                PatientPhone = "098000000",
                ProblemId = 2,
                Address = "https://betterCalm.com.uy/meeting_id/" + guid.ToString(),
                IsRemote = true,
                Date = 1,
                Duration = 1,
                Bonus = 50
            };

            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(1)).Returns(psychologist);
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            mockPsychologist.Setup(x => x.Update(1, It.IsAny<Psychologist>()));

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));
            mockConsultation.Setup(x => x.Get(newConsultation.Id)).Returns(businessLogic.ToEntity(newConsultation));

            businessLogic.CreateConsultation(newConsultation);

            mockProblem.VerifyAll();
            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateConsultationWithInvalidDurationTest()
        {
            var guid = Guid.NewGuid();
            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address = "https://betterCalm.com.uy/meeting_id/" + guid.ToString(),
                IsRemote = true,
                Date = 3,
                Duration = 0,
                Bonus = 50
            };


            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(It.IsAny<int>())).Returns(new Psychologist { });
            mockPsychologist.Setup(x => x.GetAll()).Returns(new List<Psychologist> { }.AsQueryable);

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));

            businessLogic.CreateConsultation(newConsultation);

            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateConsultationWithInvalidBonusTest()
        {
            var guid = Guid.NewGuid();
            var newConsultation = new ConsultationDTO
            {
                Id = 3,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = 1,
                Address = "https://betterCalm.com.uy/meeting_id/" + guid.ToString(),
                IsRemote = true,
                Date = 3,
                Duration = 1,
                Bonus = 10
            };


            mockProblem.Setup(x => x.Get(It.IsAny<int>())).Returns(new Problem { });

            mockPsychologist.Setup(x => x.Get(It.IsAny<int>())).Returns(new Psychologist { });
            mockPsychologist.Setup(x => x.GetAll()).Returns(new List<Psychologist> { }.AsQueryable);

            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            mockConsultation.Setup(x => x.Add(businessLogic.ToEntity(newConsultation)));

            businessLogic.CreateConsultation(newConsultation);

            mockConsultation.VerifyAll();
            mockPsychologist.VerifyAll();
        }
    }
}
