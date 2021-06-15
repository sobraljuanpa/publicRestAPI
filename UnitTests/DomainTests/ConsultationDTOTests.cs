using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Domain.DTOs;

namespace UnitTests
{
    [TestClass]
    public class ConsultationDTOTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            ConsultationDTO consultation = new ConsultationDTO();

            Assert.IsNotNull(consultation);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            ConsultationDTO consultation = new ConsultationDTO();

            consultation.Id = 1;

            Assert.AreEqual(1, consultation.Id);
        }

        [TestMethod]
        public void SetAndGetPatientNameTest()
        {
            string patientName = "Maria";

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.PatientName = patientName;

            Assert.AreEqual(patientName, consultation.PatientName);
        }

        [TestMethod]
        public void SetAndGetPatientBirthDateTest()
        {
            ConsultationDTO consultation = new ConsultationDTO();

            DateTime date = new DateTime(1998, 01, 01);
            consultation.PatientBirthDate = date;

            Assert.AreEqual(date, consultation.PatientBirthDate);
        }

        [TestMethod]
        public void SetAndGetPatientEmailTest()
        {
            string patientEmail = "maria@hotmail.com";

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.PatientEmail = patientEmail;

            Assert.AreEqual(patientEmail, consultation.PatientEmail);
        }

        [TestMethod]
        public void SetAndGetPatientPhoneTest()
        {
            string patientPhone = "099887766";

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.PatientPhone = patientPhone;

            Assert.AreEqual(patientPhone, consultation.PatientPhone);
        }

        [TestMethod]
        public void SetAndGetProblemIdTest()
        {
            int problemId = 1;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.ProblemId = problemId;

            Assert.AreEqual(problemId, consultation.ProblemId);
        }

        [TestMethod]
        public void SetAndGetPsychologistIdTest()
        {
            int psychologistId = 1;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.PsychologistId = psychologistId;

            Assert.AreEqual(psychologistId, consultation.PsychologistId);
        }

        [TestMethod]
        public void SetAndGetIsRemoteTest()
        {
            bool isRemote = false;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.IsRemote = isRemote;

            Assert.AreEqual(isRemote, consultation.IsRemote);
        }


        [TestMethod]
        public void SetAndGetAddressTest()
        {
            string address = "aaaa";

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.Address = address;

            Assert.AreEqual(address, consultation.Address);
        }

        [TestMethod]
        public void SetAndGetDateTest()
        {
            int date = 1;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.Date = date;

            Assert.AreEqual(date, consultation.Date);
        }

        [TestMethod]
        public void SetAndGetDurationTest()
        {
            int duration = 1;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.Duration = duration;

            Assert.AreEqual(duration, consultation.Duration);
        }

        [TestMethod]
        public void SetAndGetBonusTest()
        {
            int bonus = 25;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.Bonus = bonus;

            Assert.AreEqual(bonus, consultation.Bonus);
        }

        [TestMethod]
        public void SetAndGetCostTest()
        {
            int cost = 200;

            ConsultationDTO consultation = new ConsultationDTO();

            consultation.Cost = cost;

            Assert.AreEqual(cost, consultation.Cost);
        }


    }
}
