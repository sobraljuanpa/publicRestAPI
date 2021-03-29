using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTests
{
    [TestClass]
    public class ConsultationTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Consultation c = new Consultation();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Consultation c = new Consultation();

            c.Id = 1;

            Assert.AreEqual(1, c.Id);
        }

        [TestMethod]
        public void SetAndGetPatientNameTest()
        {
            Consultation c = new Consultation();

            c.PatientName = "Maria";

            Assert.AreEqual("Maria", c.PatientName);
        }

        [TestMethod]
        public void SetAndGetPatientBirthDateTest()
        {
            Consultation c = new Consultation();

            DateTime date = new DateTime(1998, 01, 01);
            c.PatientBirthDate = date;

            Assert.AreEqual(date, c.PatientBirthDate);
        }

        [TestMethod]
        public void SetAndGetPatientEmailTest()
        {
            Consultation c = new Consultation();

            c.PatientEmail = "maria@hotmail.com";

            Assert.AreEqual("maria@hotmail.com", c.PatientEmail);
        }

        [TestMethod]
        public void SetAndGetPatientPhoneTest()
        {
            Consultation c = new Consultation();

            c.PatientPhone = "099887766";

            Assert.AreEqual("099887766", c.PatientPhone);
        }


    }
}
