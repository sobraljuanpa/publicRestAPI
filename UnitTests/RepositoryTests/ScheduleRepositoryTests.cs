using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;
namespace UnitTests.RepositoryTests
{
    [TestClass]
    public class ScheduleRepositoryTests
    {

        ScheduleRepository repository;
        Mock<DbSet<Schedule>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var data = new List<Schedule>
            {
                new Schedule
                {
                    Id = 1,
                    MondayConsultations= 0,
                    TuesdayConsultations = 0,
                    WednesdayConsultations = 0,
                    ThursdayConsultations = 0,
                    FridayConsultations = 0
                },
                new Schedule
                {
                    Id = 2,
                    MondayConsultations = 5,
                    TuesdayConsultations = 5,
                    WednesdayConsultations = 5,
                    ThursdayConsultations = 5,
                    FridayConsultations = 5
                },
            }.AsQueryable();

            mockSet = new Mock<DbSet<Schedule>>();
            mockSet.As<IQueryable<Schedule>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Schedule>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Schedule>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.Schedules).Returns(mockSet.Object);

            repository = new ScheduleRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var schedules = repository.GetAll();

            int actual = schedules.ToList().Count;
            int result = 2;

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var result = new Schedule
            {
                Id = 1,
                MondayConsultations = 0,
                TuesdayConsultations = 0,
                WednesdayConsultations = 0,
                ThursdayConsultations = 0,
                FridayConsultations = 0
            };

            var schedule = repository.Get(1);

            Assert.IsNotNull(schedule);
        }

        [TestMethod]
        public void AddScheduleTest()
        {
            var schedule = new Schedule
            {
                Id = 3,
                MondayConsultations = 1,
                TuesdayConsultations = 1,
                WednesdayConsultations = 1,
                ThursdayConsultations = 1,
                FridayConsultations = 1
            };

            repository.Add(schedule);

            mockSet.Verify(v => v.Add(It.IsAny<Schedule>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateScheduleTest()
        {
            var schedule = new Schedule
            {
                Id = 1,
                MondayConsultations = 1,
                TuesdayConsultations = 1,
                WednesdayConsultations = 1,
                ThursdayConsultations = 1,
                FridayConsultations = 1
            };

            repository.Update(1, schedule);

            var modifiedSchedule = repository.Get(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Exactly(1));
            Assert.AreEqual(schedule.Id, modifiedSchedule.Id);
        }

        [TestMethod]
        public void DeleteTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }
    }
}
