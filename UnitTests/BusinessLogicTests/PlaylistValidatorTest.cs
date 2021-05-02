using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Domain;
using IDataAccess;
using BusinessLogic;
using System;

namespace UnitTests.BusinessLogicTests
{
    [TestClass]
    public class PlaylistValidatorTest
    {
        private Mock<IRepository<Playlist>> mock;
        private IEnumerable<Playlist> playlists;
        private IEnumerable<PlayableContent> contents;
        private Playlist playlist;
        private PlayableContent content;
        private PlaylistValidator validator;

        [TestInitialize]
        public void SetUp()
        {

            mock = new Mock<IRepository<Playlist>>(MockBehavior.Strict);
            validator = new PlaylistValidator(mock.Object);
            var auxCategory = new Category
            {
                Id = 3,
                Name = "Musica"
            };

            content = new PlayableContent
            {
                Id = 1,
                Author = "Buenos Muchachos",
                Category = auxCategory,
                Duration = 1.2,
                ContentURL = "http://sin-hogar.mp3",
                ImageURL = "",
                Name = "Sin hogar"
            };

            playlist = new Playlist
            {
                Id = 1,
                Category = auxCategory,
                Description = "Rock uruguayo",
                ImageURL = "",
                Name = "Rock uruguayo",
                Contents = new List<PlayableContent> { content }
            };

            contents = new List<PlayableContent> 
            { 
                content 
            }.AsQueryable();

            playlists = new List<Playlist>
            {
                playlist
            }.AsQueryable();
        }

        [TestMethod]
        public void IdInValidRangeTest()
        {
            mock.Setup(x => x.GetAll()).Returns(playlists.AsQueryable);
            validator.IdInValidRange(1);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IdNotInValidRangeTest()
        {
            mock.Setup(x => x.GetAll()).Returns(playlists.AsQueryable);
            validator.IdInValidRange(0);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NonExistingPlaylistTest()
        {
            mock.Setup(x => x.GetAll()).Returns(playlists.AsQueryable);
            Playlist playlist = null;
            validator.Exists(playlist);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AlreadyOnPlaylistTest()
        {
            mock.Setup(x => x.GetAll()).Returns(playlists.AsQueryable);
            validator.AlreadyOnPlaylist(playlist,content);
            mock.VerifyAll();
        }

        [TestMethod]
        public void ValidPlaylistTest()
        {
            mock.Setup(x => x.GetAll()).Returns(playlists.AsQueryable);
            var newPlaylist = new Playlist
            {
                Description = "Nothing",
                Name = "Nothing"
            };
            validator.ValidPlaylist(newPlaylist);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidPlaylistTest()
        {
            mock.Setup(x => x.GetAll()).Returns(playlists.AsQueryable);
            validator.ValidPlaylist(playlist);
            mock.VerifyAll();
        }
    }
}
