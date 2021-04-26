using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;
using BusinessLogic;
using System;

namespace UnitTests.BusinessLogicTests
{
    [TestClass]
    public class ConsultationValidatorTests
    {

        private Mock<IRepository<Consultation>> mockConsultation;
        private Mock<IRepository<Psychologist>> mockPsychologist;
        private IEnumerable<Consultation> consultations;
        private IEnumerable<Psychologist> psychologists;
        private Consultation consultation;
        private Psychologist psychologist;
        private Problem problem;
        private ConsultationValidator validator;


        [TestInitialize]
        public void SetUp()
        {
            mockConsultation = new Mock<IRepository<Consultation>>(MockBehavior.Strict);
            mockPsychologist = new Mock<IRepository<Psychologist>>(MockBehavior.Strict);
            validator = new ConsultationValidator(mockConsultation.Object, mockPsychologist.Object);

            problem = new Problem
            {
                Id = 1,
                Name = "Depresión"
            };
            var schedule = new Schedule
            {
                MondayConsultations = 5,
                TuesdayConsultations = 0,
                WednesdayConsultations = 0,
                ThursdayConsultations = 0,
                FridayConsultations = 1
            };

            var psychologistExperties = new List<Problem>
            {
                problem
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
                Address = "https://betterCalm.com.uy/meeting_id/codigo",
                IsRemote = true,
                Date = 1

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
                    Date = 2
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
        }

        [TestMethod]
        public void IdValidRangeTest()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable);
            validator.IdValidRange(2);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IdInvalidRangeTest()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable);
            validator.IdValidRange(-1);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void FindConsultationsTest()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            validator.FindConsultations(1);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmptyConsultationsTest()
        {
            mockConsultation.Setup(x => x.GetAll()).Returns(consultations.AsQueryable());
            validator.FindConsultations(2);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void GetPsychologistWithExpertiseTest()
        {
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            validator.PsychologistsWithExpertise(problem);

            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        public void GetOldestPsychologistTest()
        {
            validator.GetExpert(psychologists.ToList());

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void AssignPsychologistToConsultationTest()
        {
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            validator.AssignPsychologist(consultation);

            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        public void IdValidRangePsychologistTest()
        {
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            validator.IdValidRangePs(psychologist.Id);

            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IdInvalidRangePsychologistTest()
        {
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            validator.IdValidRangePs(-1);

            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        public void ValidScheduleTest()
        {
            validator.ValidSchedule(psychologist);

            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidScheduleTest()
        {
            var newPsychologist = new Psychologist
            {
                Id = 3,
                PsychologistName = "",
                PsychologistSurname = "",
                ActiveYears = 4,
                Schedule = null,
                Expertise = null
            };
            mockPsychologist.Setup(x => x.GetAll()).Returns(psychologists.AsQueryable());
            validator.ValidSchedule(newPsychologist);

            mockPsychologist.VerifyAll();
        }

        [TestMethod]
        public void ValidRemoteAddressTest()
        {
            validator.ValidAddress(consultation);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidRemoteAddressTest()
        {
            var newConsultation = new Consultation
            {
                Id = 1,
                PatientName = "Sofia",
                PatientBirthDate = new DateTime(1998, 01, 01),
                PatientEmail = "sofia@hotmial.com",
                PatientPhone = "098999999",
                Problem = problem,
                Psychologist = psychologist,
                Address = "aaaa.aa",
                IsRemote = true,
                Date = 1
            };

            validator.ValidAddress(newConsultation);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FullScheduleTest()
        {
            var newConsultation = new Consultation
            {
                Id = 3,
                PatientName = "Matias",
                PatientBirthDate = new DateTime(1990, 01, 01),
                PatientEmail = "matias@hotmial.com",
                PatientPhone = "098000000",
                Problem = problem,
                Psychologist = psychologist,
                Address = "https://betterCalm.com.uy/meeting_id/codigo",
                IsRemote = true,
                Date = 0

            };

            validator.FullSchedule(newConsultation);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void FreeScheduleTest()
        {
            validator.FullSchedule(consultation);

            mockConsultation.VerifyAll();
        }

        [TestMethod]
        public void AddToScheduleTest()
        {
            mockPsychologist.Setup(x => x.Update(psychologist.Id,psychologist));
            validator.AddToSchedule(1,psychologist);

            mockPsychologist.VerifyAll();
        }
    }

}
