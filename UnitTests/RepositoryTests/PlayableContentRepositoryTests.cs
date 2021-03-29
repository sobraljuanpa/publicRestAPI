using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.EntityFrameworkCore;

using Moq;

using Domain;
using DataAccess;
using IDataAccess;

namespace UnitTests
{
    [TestClass]
    public class PlayableContentRepositoryTests
    {

        PlayableContentRepository repository;
        Mock<DbSet<PlayableContent>> mockSet;
        Mock<Context> mockContext;
        DbContextOptions<Context> DbOptions;

        [TestInitialize]
        public void SetUp()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var data = new List<PlayableContent>
            {
                new PlayableContent { Id = 1, Author = "Buenos Muchachos", Category = auxCategory, Duration = 1.2, ContentURL = "http://sin-hogar.mp3", ImageURL = "", Name = "Sin hogar"},
                new PlayableContent { Id = 2, Author = "Buitres", Category = auxCategory, Duration = 2.2, ContentURL = "http://cadillac-solitario.mp3", ImageURL = "", Name = "Cadillac solitario"}
            }.AsQueryable();

            mockSet = new Mock<DbSet<PlayableContent>>();
            mockSet.As<IQueryable<PlayableContent>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PlayableContent>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PlayableContent>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(pk => data.FirstOrDefault(d => d.Id == (int)pk[0]));

            DbOptions = new DbContextOptions<Context>();
            mockContext = new Mock<Context>(DbOptions);
            mockContext.Setup(v => v.PlayableContents).Returns(mockSet.Object);

            repository = new PlayableContentRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var contents = repository.GetAll();

            Assert.AreEqual(2, contents.ToList().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var content = repository.Get(1);

            Assert.AreEqual("Sin hogar", content.Name);
        }

        [TestMethod]
        public void AddContentTest()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var content = new PlayableContent { Id = 3, Author = "The Smiths", Category = auxCategory, Duration = 3.2, ContentURL = "http://this-charming-man.mp3", ImageURL = "", Name = "This Charming Man" };
            repository.Add(content);

            mockSet.Verify(v => v.Add(It.IsAny<PlayableContent>()), Times.Once());
            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateContentTest()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var content = new PlayableContent { Id = 2, Author = "Buitres", Category = auxCategory, Duration = 3.2, ContentURL = "http://carretera-perdida.mp3", ImageURL = "", Name = "Carretera Perdida" };
            repository.Update(2, content);
            var modifiedContent = repository.Get(2);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
            Assert.AreEqual("Carretera Perdida", modifiedContent.Name);
        }

        [TestMethod]
        public void DeleteContentTest()
        {
            repository.Delete(1);

            mockContext.Verify(e => e.SaveChanges(), Times.Once());
        }
    }
}
