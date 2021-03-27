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
            PlayableContent p = new PlayableContent();

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            PlayableContent p = new PlayableContent();

            p.Id = 1;

            Assert.AreEqual(1, p.Id);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            PlayableContent p = new PlayableContent();

            p.Name = "Sin mas";

            Assert.AreEqual("Sin mas", p.Name);
        }

        [TestMethod]
        public void SetAndGetImageURLTest()
        {
            PlayableContent p = new PlayableContent();

            p.ImageURL = "http://imagen";

            Assert.AreEqual("http://imagen", p.ImageURL);
        }

        [TestMethod]
        public void SetAndGetDurationTest()
        {
            PlayableContent p = new PlayableContent();

            p.Duration = 1.2;

            Assert.AreEqual(1.2, p.Duration);
        }

        [TestMethod]
        public void SetAndGetAuthorTest()
        {
            PlayableContent p = new PlayableContent();

            p.Author = "Buenos Muchachos";

            Assert.AreEqual("Buenos Muchachos", p.Author);
        }

        [TestMethod]
        public void SetAndGetContentURLTest()
        {
            PlayableContent p = new PlayableContent();

            p.ContentURL = "http://sin-mas";

            Assert.AreEqual("http://sin-mas", p.ContentURL);
        }
    }
}
