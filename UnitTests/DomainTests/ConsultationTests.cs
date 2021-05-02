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
            Consultation consultation = new Consultation();

            Assert.IsNotNull(consultation);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Consultation consultation = new Consultation();

            consultation.Id = 1;

            Assert.AreEqual(1, consultation.Id);
        }

        [TestMethod]
        public void SetAndGetPatientNameTest()
        {
            string patientName = "Maria";

            Consultation consultation = new Consultation();

            consultation.PatientName = patientName;

            Assert.AreEqual(patientName, consultation.PatientName);
        }

        [TestMethod]
        public void SetAndGetPatientBirthDateTest()
        {
            Consultation consultation = new Consultation();

            DateTime date = new DateTime(1998, 01, 01);
            consultation.PatientBirthDate = date;

            Assert.AreEqual(date, consultation.PatientBirthDate);
        }

        [TestMethod]
        public void SetAndGetPatientEmailTest()
        {
            string patientEmail = "maria@hotmail.com";

            Consultation consultation = new Consultation();

            consultation.PatientEmail = patientEmail;

            Assert.AreEqual(patientEmail, consultation.PatientEmail);
        }

        [TestMethod]
        public void SetAndGetPatientPhoneTest()
        {
            string patientPhone = "099887766";

            Consultation consultation = new Consultation();

            consultation.PatientPhone = patientPhone;

            Assert.AreEqual(patientPhone, consultation.PatientPhone);
        }


    }
}
