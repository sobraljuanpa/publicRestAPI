using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain;

namespace UnitTests.DomainTests
{
    [TestClass]
    class ScheduleTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Schedule schedule = new Schedule();

            Assert.IsNotNull(schedule);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Schedule schedule = new Schedule();

            schedule.Id = 1;

            Assert.AreEqual(1, schedule.Id);
        }

        [TestMethod]
        public void SetAndGetMondayConsultationsTest()
        {
            int result = 2;

            Schedule schedule = new Schedule();

            schedule.MondayConsultations = 2;

            Assert.AreEqual(result, schedule.MondayConsultations);
        }

        [TestMethod]
        public void SetAndGetTuesdayConsultationsTest()
        {
            int result = 1;

            Schedule schedule = new Schedule();

            schedule.TuesdayConsultations = 1;

            Assert.AreEqual(result, schedule.TuesdayConsultations);
        }

        [TestMethod]
        public void SetAndGetWednesdayConsultationsTest()
        {
            int result = 0;

            Schedule schedule = new Schedule();

            schedule.WednesdayConsultations = 0;

            Assert.AreEqual(result, schedule.WednesdayConsultations);
        }

        [TestMethod]
        public void SetAndGetThurdayConsultationsTest()
        {
            int result = 4;

            Schedule schedule = new Schedule();

            schedule.ThursdayConsultations = 4;

            Assert.AreEqual(result, schedule.ThursdayConsultations);
        }

        [TestMethod]
        public void SetAndGetFridayConsultationsTest()
        {
            int result = 2;

            Schedule schedule = new Schedule();

            schedule.FridayConsultations = 2;

            Assert.AreEqual(result, schedule.FridayConsultations);
        }
    }
}
