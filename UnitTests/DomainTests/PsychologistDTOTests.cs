using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.DTOs;
using Domain;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class PsychologistDTOTests
    {

        [TestMethod]
        public void EmptyConstructorTest()
        {
            PsychologistDTO psychologist = new PsychologistDTO();

            Assert.IsNotNull(psychologist);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.Id = 1;

            Assert.AreEqual(1, psychologist.Id);
        }

        [TestMethod]
        public void SetAndGetPsychologistNameTest()
        {
            string name = "Florencia";

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.PsychologistName = name;

            Assert.AreEqual(name, psychologist.PsychologistName);
        }

        [TestMethod]
        public void SetAndGetPsychologistSurnameTest()
        {
            string surname = "Lopez";

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.PsychologistSurname = surname;

            Assert.AreEqual(surname, psychologist.PsychologistSurname);
        }

        [TestMethod]
        public void SetAndGetIsRemoteTest()
        {
            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.IsRemote = true;

            Assert.IsTrue(psychologist.IsRemote);
        }

        [TestMethod]
        public void SetAndGetAddressTest()
        {
            string address = "Bulevar General Artigas 1199";

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.Address = address;

            Assert.AreEqual(address, psychologist.Address);
        }

        [TestMethod]
        public void SetAndGetActiveYearsTest()
        {
            int activeYears = 3;

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.ActiveYears = activeYears;

            Assert.AreEqual(activeYears, psychologist.ActiveYears);
        }

        [TestMethod]
        public void SetAndGetScheduleIdTest()
        {
            int scheduleId = 1;

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.ScheduleId = scheduleId;

            Assert.AreEqual(scheduleId, psychologist.ScheduleId);
        }

        [TestMethod]
        public void SetAndGetFeeTest()
        {
            int fee = 1000;

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.Fee = fee;

            Assert.AreEqual(fee, psychologist.Fee);
        }


        [TestMethod]
        public void SetExpertiseId1Test()
        {
            int expertiseId1 = 1;

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.ExpertiseId1 = expertiseId1;

            Assert.AreEqual(expertiseId1, psychologist.ExpertiseId1);
        }

        [TestMethod]
        public void SetExpertiseId2Test()
        {
            int expertiseId2 = 2;

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.ExpertiseId2 = expertiseId2;

            Assert.AreEqual(expertiseId2, psychologist.ExpertiseId2);
        }

        [TestMethod]
        public void SetExpertiseId3Test()
        {
            int expertiseId3 = 3;

            PsychologistDTO psychologist = new PsychologistDTO();

            psychologist.ExpertiseId3 = expertiseId3;

            Assert.AreEqual(expertiseId3, psychologist.ExpertiseId3);
        }
    }
}
