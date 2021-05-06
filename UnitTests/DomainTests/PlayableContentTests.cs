using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain;

namespace UnitTests
{
    [TestClass]
    public class PlayableContentTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            PlayableContent content = new PlayableContent();

            Assert.IsNotNull(content);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            PlayableContent content = new PlayableContent();

            content.Id = 1;

            Assert.AreEqual(1, content.Id);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            string name = "Sin mas";

            PlayableContent content = new PlayableContent();

            content.Name = name;

            Assert.AreEqual(name, content.Name);
        }

        [TestMethod]
        public void SetAndGetImageURLTest()
        {
            string imageURL = "http://imagen";

            PlayableContent content = new PlayableContent();

            content.ImageURL = imageURL;

            Assert.AreEqual(imageURL, content.ImageURL);
        }

        [TestMethod]
        public void SetAndGetCategoryTest()
        {
            Category category = new Category();
            PlayableContent content = new PlayableContent();

            content.Category = category;

            Assert.AreEqual(category, content.Category);
        }

        [TestMethod]
        public void SetAndGetDurationTest()
        {
            double duration = 1.2;

            PlayableContent content = new PlayableContent();

            content.Duration = duration;

            Assert.AreEqual(duration, content.Duration);
        }

        [TestMethod]
        public void SetAndGetAuthorTest()
        {
            string author = "Buenos Muchachos";

            PlayableContent content = new PlayableContent();

            content.Author = author;

            Assert.AreEqual(author, content.Author);
        }

        [TestMethod]
        public void SetAndGetContentURLTest()
        {
            string contentURL = "http://sin-mas";

            PlayableContent content = new PlayableContent();

            content.ContentURL = contentURL;

            Assert.AreEqual(contentURL, content.ContentURL);
        }
    }
}
