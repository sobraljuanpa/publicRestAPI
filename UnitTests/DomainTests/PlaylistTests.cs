using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain;

namespace UnitTests
{
    [TestClass]
    public class PlaylistTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Playlist p = new Playlist();

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Playlist p = new Playlist();

            p.Id = 1;

            Assert.AreEqual(1, p.Id);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            Playlist p = new Playlist();

            p.Name = "Rock uruguayo";

            Assert.AreEqual("Rock uruguayo", p.Name);
        }

        [TestMethod]
        public void SetAndGetImageURLTest()
        {
            Playlist p = new Playlist();

            p.ImageURL = "http://imagen";

            Assert.AreEqual("http://imagen", p.ImageURL);
        }

        [TestMethod]
        public void SetAndGetCategoryTest()
        {
            Category c = new Category();
            Playlist p = new Playlist();

            p.Category = c;

            Assert.AreEqual(c, p.Category);
        }

        [TestMethod]
        public void SetAndGetDescriptionTest()
        {
            Playlist p = new Playlist();

            p.Description = "Lo mejor del rock nacional";

            Assert.AreEqual("Lo mejor del rock nacional", p.Description);
        }

        [TestMethod]
        public void SetContentsTest()
        {
            Playlist p = new Playlist();
            p.Contents = new System.Collections.Generic.List<PlayableContent>();
            PlayableContent c = new PlayableContent();

            p.Contents.Add(c);

            Assert.AreEqual(p.Contents[0], c);
        }
    }
}
