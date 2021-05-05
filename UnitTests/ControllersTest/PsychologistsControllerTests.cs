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

            expertiseDepression = new Problem 
            { 
                Id = 1,
                Name = "Depresión" 
            };
            expertiseStress = new Problem 
            { 
                Id = 2, 
                Name = "Estrés" 
            };
            data = new List<Psychologist>
            {
                new Psychologist 
                { 
                    Id = 1, 
                    PsychologistName = "Martin", 
                    PsychologistSurname = "Perez", 
                    IsRemote = true, 
                    Address = "1234567", 
                    Expertise = new List<Problem> { expertiseDepression } 
                },
                new Psychologist 
                { 
                    Id = 2, 
                    PsychologistName = "María", 
                    PsychologistSurname = "Lopez", 
                    IsRemote = false, 
                    Address = "", 
                    Expertise = new List<Problem> { expertiseStress }
                }
            }.AsQueryable();
        }

        [TestMethod]
        public void AddPsychologistTest()
        {
            Psychologist newPsychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule 
                { 
                    MondayConsultations = 0, 
                    TuesdayConsultations = 0, 
                    WednesdayConsultations = 0, 
                    ThursdayConsultations = 0, 
                    FridayConsultations = 0 
                }
            };

            mock.Setup(x => x.AddPsychologist(newPsychologist)).
                Returns(newPsychologist);

            var result = controller.AddPsychologist(newPsychologist);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(201, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddPsychologistFailTest()
        {
            Psychologist newPsychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule 
                { 
                    MondayConsultations = 0, 
                    TuesdayConsultations = 0, 
                    WednesdayConsultations = 0, 
                    ThursdayConsultations = 0, 
                    FridayConsultations = 0 
                }
            };

            mock.Setup(x => x.AddPsychologist(newPsychologist)).
                Throws(new Exception { });

            var result = controller.AddPsychologist(newPsychologist);
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
            mock.Setup(x => x.DeletePsychologist(1)).
                Throws(new Exception { });

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
                    Schedule = new Schedule 
                    { 
                        MondayConsultations = 0, 
                        TuesdayConsultations = 0, 
                        WednesdayConsultations = 0, 
                        ThursdayConsultations = 0, 
                        FridayConsultations = 0 
                    }
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
        public void AddProblemToPsychologistTest()
        {
            Psychologist newPsychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule
                {
                    MondayConsultations = 0,
                    TuesdayConsultations = 0,
                    WednesdayConsultations = 0,
                    ThursdayConsultations = 0,
                    FridayConsultations = 0
                }
            };

            mock.Setup(x => x.AddProblemToPsychologist(newPsychologist,expertiseDepression.Id));

            var result = controller.AddProblemToPsychologist(expertiseDepression.Id, newPsychologist);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void AddAlreadyExistingProblemToPsychologistTest()
        {
            Psychologist newPsychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule
                {
                    MondayConsultations = 0,
                    TuesdayConsultations = 0,
                    WednesdayConsultations = 0,
                    ThursdayConsultations = 0,
                    FridayConsultations = 0
                }

                
            };

            mock.Setup(x => x.AddProblemToPsychologist(newPsychologist, 
                expertiseDepression.Id)).Throws(new Exception());

            var result = controller.AddProblemToPsychologist(expertiseDepression.Id, newPsychologist);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(400, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateAdministratorTest()
        {
            Psychologist newPsychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule 
                { 
                    MondayConsultations = 0, 
                    TuesdayConsultations = 0, 
                    WednesdayConsultations = 0, 
                    ThursdayConsultations = 0, 
                    FridayConsultations = 0 
                }
            };

            mock.Setup(x => x.UpdatePsychologist(1, newPsychologist));

            var result = controller.UpdatePsychologist(1, newPsychologist);
            var objectResult = result as OkResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(200, statusCode);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateNonExistingAdministratorTest()
        {
            Psychologist newPsychologist = new Psychologist
            {
                PsychologistName = "juan",
                Address = "juan 1234",
                PsychologistSurname = "perez",
                Expertise = new List<Problem> { expertiseStress },
                IsRemote = false,
                Schedule = new Schedule 
                { 
                    MondayConsultations = 0, 
                    TuesdayConsultations = 0, 
                    WednesdayConsultations = 0, 
                    ThursdayConsultations = 0, 
                    FridayConsultations = 0 
                }
            };
            mock.Setup(x => x.UpdatePsychologist(0, newPsychologist)).
                Throws(new Exception());

            var result = controller.UpdatePsychologist(0, newPsychologist);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            Assert.AreEqual(404, statusCode);
            mock.VerifyAll();
        }
    }
}
