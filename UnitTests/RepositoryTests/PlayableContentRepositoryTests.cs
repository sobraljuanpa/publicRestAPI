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
    public class PlayableContentRepositoryTests
    {

        PlayableContentRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            var auxCategory = new Category { Id = 3, Name = "Musica" };
            var data = new List<PlayableContent>
            {
                new PlayableContent { Id = 1, Author = "Buenos Muchachos", Category = auxCategory, Duration = 1.2, ContentURL = "http://sin-hogar.mp3", ImageURL = "", Name = "Sin hogar"},
                new PlayableContent { Id = 2, Author = "Buitres", Category = auxCategory, Duration = 2.2, ContentURL = "http://cadillac-solitario.mp3", ImageURL = "", Name = "Cadillac solitario"}
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("BetterCalmDB")
                .Options;

            Context context = new Context(options);

            context.Database.EnsureDeleted();
            context.Set<PlayableContent>().AddRange(data);
            context.SaveChanges();

            repository = new PlayableContentRepository(context);
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
            var content = new PlayableContent { Id = 3, Author = "The Smiths",CategoryId=3, Duration = 3.2, ContentURL = "http://this-charming-man.mp3", ImageURL = "", Name = "This Charming Man" };
            repository.Add(content);

            var count = repository.GetAll().Count();

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void UpdateContentTest()
        {
            var content = new PlayableContent { Id = 2, Author = "Buitres", Duration = 3.2, ContentURL = "http://carretera-perdida.mp3", ImageURL = "", Name = "Carretera Perdida" };

            repository.Update(2, content);

            var modifiedContent = repository.Get(2);

            Assert.AreEqual("Carretera Perdida", modifiedContent.Name);
        }

        [TestMethod]
        public void DeleteContentTest()
        {
            repository.Delete(1);

            var count = repository.GetAll().Count();

            Assert.AreEqual(1, count);
        }
    }
}
