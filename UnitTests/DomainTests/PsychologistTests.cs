using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTests
{
    [TestClass]
    public class PsychologistTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Psychologist psychologist = new Psychologist();

            Assert.IsNotNull(psychologist);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Psychologist psychologist = new Psychologist();

            psychologist.Id = 1;

            Assert.AreEqual(1, psychologist.Id);
        }

        [TestMethod]
        public void SetAndGetPsychologistNameTest()
        {
            string name = "Florencia";

            Psychologist psychologist = new Psychologist();

            psychologist.PsychologistName = name;

            Assert.AreEqual(name, psychologist.PsychologistName);
        }

        [TestMethod]
        public void SetAndGetPsychologistSurnameTest()
        {
            string surname = "Lopez";

            Psychologist psychologist = new Psychologist();

            psychologist.PsychologistSurname = surname;

            Assert.AreEqual(surname, psychologist.PsychologistSurname);
        }

        [TestMethod]
        public void SetAndGetIsRemoteTest()
        {
            Psychologist psychologist = new Psychologist();

            psychologist.IsRemote = true;

            Assert.IsTrue(psychologist.IsRemote);
        }

        [TestMethod]
        public void SetAndGetAddressTest()
        {
            string address = "Bulevar General Artigas 1199";

            Psychologist psychologist = new Psychologist();

            psychologist.Address = address;

            Assert.AreEqual(address, psychologist.Address);
        }

        [TestMethod]
        public void SetAndGetActiveYearsTest()
        {
            int activeYears = 3;

            Psychologist psychologist = new Psychologist();

            psychologist.ActiveYears = activeYears;

            Assert.AreEqual(activeYears, psychologist.ActiveYears);
        }

        [TestMethod]
        public void SetAndGetScheduleIdTest()
        {
            int scheduleId = 1;

            Psychologist psychologist = new Psychologist();

            psychologist.ScheduleId = scheduleId;

            Assert.AreEqual(scheduleId, psychologist.ScheduleId);
        }

        [TestMethod]
        public void SetAndGetScheduleTest()
        {
            Schedule schedule = new Schedule();
            Psychologist psychologist = new Psychologist();

            psychologist.Schedule = schedule;

            Assert.IsNotNull(psychologist.Schedule);
        }

        [TestMethod]
        public void SetAndGetFeeTest()
        {
            int fee = 1000;

            Psychologist psychologist = new Psychologist();

            psychologist.Fee = fee;

            Assert.AreEqual(fee, psychologist.Fee);
        }


        [TestMethod]
        public void SetExpertiseTest()
        {
            Psychologist psychologist = new Psychologist();
            psychologist.Expertise = new System.Collections.Generic.List<Problem>();
            Problem problem = new Problem();

            psychologist.Expertise.Add(problem);
            int result = psychologist.Expertise.Count;

            Assert.AreEqual(result, 1);
        }
    }
}
