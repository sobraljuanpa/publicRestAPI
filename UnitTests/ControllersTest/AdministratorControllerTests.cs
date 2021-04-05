using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using WebAPI.Controllers;
using DataAccess;
using IDataAccess;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.ControllersTest
{
    public class AdministratorControllerTests
    {

        Mock<DbSet<Administrator>> administratorMockSet;
        AdministratorRepository administratorRepository;
        AdministratorBL administratorBL;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;
        AdministratorsController controller;

        [TestInitialize]
        public void SetUp()
        {

            var dataAdministrator = new List<Administrator>
            {
                new Administrator 
                {
                    Id = 1,
                    Email = "admin@admin.admin",
                    Name = "admin",
                    Password = "admin"
                }
            }.AsQueryable();

            administratorMockSet = new Mock<DbSet<Administrator>>();
            administratorMockSet.As<IQueryable<Administrator>>().Setup(m => m.Expression).Returns(dataAdministrator.Expression);
            administratorMockSet.As<IQueryable<Administrator>>().Setup(m => m.ElementType).Returns(dataAdministrator.ElementType);
            administratorMockSet.As<IQueryable<Administrator>>().Setup(m => m.GetEnumerator()).Returns(dataAdministrator.GetEnumerator());
            administratorMockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => dataAdministrator.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);

            mockContext.Setup(v => v.Administrators).Returns(administratorMockSet.Object);
            administratorRepository = new AdministratorRepository(mockContext.Object);

            administratorBL = new AdministratorBL(administratorRepository);
            controller = new AdministratorsController(administratorBL);
        }
    }
}
