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
            Playlist playlist = new Playlist();

            Assert.IsNotNull(playlist);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Playlist playlist = new Playlist();

            playlist.Id = 1;

            Assert.AreEqual(1, playlist.Id);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            string name = "Rock uruguayo";

            Playlist playlist = new Playlist();

            playlist.Name = name;

            Assert.AreEqual(name, playlist.Name);
        }

        [TestMethod]
        public void SetAndGetImageURLTest()
        {
            string contentURL = "http://imagen";

            Playlist playlist = new Playlist();

            playlist.ImageURL = contentURL;

            Assert.AreEqual(contentURL, playlist.ImageURL);
        }

        [TestMethod]
        public void SetAndGetCategoryTest()
        {
            Category category = new Category();
            Playlist playlist = new Playlist();

            playlist.Category = category;

            Assert.AreEqual(category, playlist.Category);
        }

        [TestMethod]
        public void SetAndGetDescriptionTest()
        {
            string description = "Lo mejor del rock nacional";

            Playlist playlist = new Playlist();

            playlist.Description = description;

            Assert.AreEqual(description, playlist.Description);
        }

        [TestMethod]
        public void SetContentsTest()
        {
            Playlist playlist = new Playlist();
            playlist.Contents = new System.Collections.Generic.List<PlayableContent>();
            PlayableContent content = new PlayableContent();

            playlist.Contents.Add(content);
            int result = playlist.Contents.Count;

            Assert.AreEqual(result, 1);
        }
    }
}
