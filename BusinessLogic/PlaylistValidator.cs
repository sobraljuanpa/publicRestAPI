using System.Linq;
using System;

using IDataAccess;
using Domain;

namespace BusinessLogic
{
    public class PlaylistValidator
    {
        private readonly IRepository<Playlist> playlistRepository;

        public PlaylistValidator(IRepository<Playlist> repository)
        {
            playlistRepository = repository;
        }
        //public void IdInValidRange(int id)
        //{
        //    if (id <= 0 && id <= playlistRepository.GetAll().ToList().FindLast(x => x.Id != 0).Id)
        //    { }
        //    else
        //    {
        //        throw new Exception("No playlist associated to given id");
        //    }
        //}

        public void IdInValidRange(int id)
        {
            if (id <= 0 && id > playlistRepository.GetAll().ToList().Count())
            {
                throw new Exception("No playlist associated to given id");
            }
        }

        public void Exists(Playlist playlist)
        {
            if (playlist == null)
            {
                throw new Exception("There's no playlist associated to given id");
            }
        }

        public void AlreadyOnPlaylist (Playlist playlist, PlayableContent content)
        {
            foreach (PlayableContent auxContent in playlist.Contents)
            {
                if (content.Name == auxContent.Name && content.Author == auxContent.Author)
                {
                    throw new Exception($"The playable content you are trying to add already exists at index {auxContent.Id}");
                }

            }
        }

        public void ValidPlaylist (Playlist playlist)
        {
            foreach (Playlist auxPlaylist in playlistRepository.GetAll().ToList())
            {
                if (playlist.Name == auxPlaylist.Name && playlist.Description == auxPlaylist.Description)
                {
                    throw new Exception($"The playlist you are trying to create already exists at index {auxPlaylist.Id}");
                }
            }
        }
    }
}
