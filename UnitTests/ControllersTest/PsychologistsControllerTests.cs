using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using WebAPI.Controllers;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTests
{
    [TestClass]

    public class PsychologistControllerTests
    {

        private Mock<IPsychologistBL> mock;
        private PsychologistsController controller;
        private IEnumerable<Psychologist> data;
        private Problem expertiseStress;
        private Problem expertiseDepression;

        [TestInitialize]
        public void SetUp()
        {
            mock = new Mock<IPsychologistBL>(MockBehavior.Strict);
            controller = new PsychologistsController(mock.Object);

            expertiseDepression = new Problem { Id = 1, Name = "Depresión" };
            expertiseStress = new Problem { Id = 2, Name = "Estrés" };
            data = new List<Psychologist>
            {
                new Psychologist { Id = 1, PsychologistName = "Martin", PsychologistSurname = "Perez", IsRemote = true, Address = "1234567", Expertise = new List<Problem> { expertiseDepression } },
                new Psychologist { Id = 2, PsychologistName = "María", PsychologistSurname = "Lopez", IsRemote = false, Address = "", Expertise = new List<Problem> { expertiseStress } }
            }.AsQueryable();


        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
            };

            mock.Setup(x => x.AddPsychologist(p)).Returns(p);
            var result = controller.AddPsychologist(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddPsychologistFailTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
            };

            mock.Setup(x => x.AddPsychologist(p)).Throws(new Exception { });
            var result = controller.AddPsychologist(p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeletePsychologistTest()
        {
            mock.Setup(x => x.DeletePsychologist(1));
            var result = controller.DeletePsychologist(1);

            var objectResult = result as NoContentResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(204, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void DeletePsychologistFailTest()
        {
            mock.Setup(x => x.DeletePsychologist(1)).Throws(new Exception { });
            var result = controller.DeletePsychologist(1);

            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAdministratorByIdTest()
        {
            mock.Setup(x => x.GetPsychologist(1))
                .Returns(new Psychologist
                {
                    PsychologistName = "juan",
                    Address = "juan 1234",
                    PsychologistSurname = "perez",
                    Expertise = new List<Problem> { expertiseStress },
                    IsRemote = false,
                    Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
                });
            var result = controller.GetPsychologistById(1);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetNonExistingAdministratorByIdTest()
        {
            mock.Setup(x => x.GetPsychologist(0))
                .Throws(new Exception());
            var result = controller.GetPsychologistById(0);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateAdministratorTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
            };
            mock.Setup(x => x.UpdatePsychologist(1, p));
            var result = controller.UpdatePsychologist(1, p);
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateNonExistingAdministratorTest()
        {
            Psychologist p = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule { MondayConsultations = 0, TuesdayConsultations = 0, WednesdayConsultations = 0, ThursdayConsultations = 0, FridayConsultations = 0 }
            };
            mock.Setup(x => x.UpdatePsychologist(0, p)).Throws(new Exception());
            var result = controller.UpdatePsychologist(0, p);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }
    }
}
