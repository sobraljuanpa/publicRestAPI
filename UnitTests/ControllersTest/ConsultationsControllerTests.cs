﻿using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using Domain.DTOs;
using WebAPI.Controllers;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTest
{
    [TestClass]

    public class ConsultationsControllerTests
    {

        private Mock<IConsultationBL> mock;
        private ConsultationDTO consultation;
        private Psychologist psychologist;
        private Problem problem;
        private IEnumerable<ConsultationDTO> consultations;
        private ConsultationsController controller;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IConsultationBL>(MockBehavior.Strict);

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
            consultation = new ConsultationDTO
            {
                Id = 1,
                PatientName = "Matias",
                PatientBirthDate = new DateTime(1990, 01, 01),
                PatientEmail = "matias@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = problem.Id,
                PsychologistId = psychologist.Id,
                Address = "",
                IsRemote = false,
                Date = 2,
                Duration = 1,
                Bonus = 10
            };
            consultations = new List<ConsultationDTO>
            {
                consultation

            }.AsQueryable();

            controller = new ConsultationsController(mock.Object);
        }

        [TestMethod]
        public void GetConsultationTest()
        {
            mock.Setup(x => x.Get(1)).Returns(consultation);

            var result = controller.GetConsultation(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetConsultationByInvalidIdTest()
        {
            mock.Setup(x => x.Get(-1)).Throws(new Exception());

            var result = controller.GetConsultation(-1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetConsultationsTest()
        {
            mock.Setup(x => x.GetConsultations()).
                Returns(consultations.ToList());

            var result = controller.GetConsultations();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetConsultationsByPsychologistTest()
        {
            var auxConsultation = new Consultation
            {
                Id = 1,
                PatientName = "Matias",
                PatientBirthDate = new DateTime(1990, 01, 01),
                PatientEmail = "matias@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = problem.Id,
                Psychologist = psychologist,
                Address = "",
                IsRemote = false,
                Date = 2,
                Duration = 1,
                Bonus = 10
            };

            var auxConsultations = new List<Consultation> 
            {
                auxConsultation
            };



            mock.Setup(x => x.GetConsultationsByPsychologist(1)).
                Returns(auxConsultations.ToList());

            var result = controller.GetConsultationsByPsychologist(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetConsultationsByInvalidPsychologistTest()
        {
            mock.Setup(x => x.GetConsultationsByPsychologist(-1)).
                Throws(new Exception());

            var result = controller.GetConsultationsByPsychologist(-1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void CreateConsultationsTest()
        {
            var newConsultation = new Consultation
            {
                Id = 2,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                Problem = problem,
                Psychologist = psychologist,
                Address = "https://betterCalm.com.uy/meeting_id/codigo",
                IsRemote = true,
                Date = 3,
                Duration = 1,
                Bonus = 10
            };

            var auxConsultation = new ConsultationDTO
            {
                Id = 2,
                PatientName = "Nicolas",
                PatientBirthDate = new DateTime(1992, 01, 01),
                PatientEmail = "nico@hotmial.com",
                PatientPhone = "098000000",
                ProblemId = problem.Id,
                PsychologistId = psychologist.Id,
                Address = "https://betterCalm.com.uy/meeting_id/codigo",
                IsRemote = true,
                Date = 3,
                Duration = 1,
                Bonus = 10
            };

            mock.Setup(x => x.CreateConsultation(auxConsultation)).
                Returns(newConsultation);

            var result = controller.CreateConsultation(auxConsultation);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
        }

        [TestMethod]
        public void CreateInvalidConsultationTest()
        {
            ConsultationDTO newConsultation = null;

            mock.Setup(x => x.CreateConsultation(newConsultation)).
                Throws(new Exception());

            var result = controller.CreateConsultation(newConsultation);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
        }
    }
}
