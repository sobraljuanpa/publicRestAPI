using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic 
{

    public class PlayerBL : IPlayerBL
    {

        private readonly IRepository<Category> categoryRepository;

        private readonly IRepository<PlayableContent> contentRepository;

        private readonly PlayableContentValidator contentValidator;

        private readonly IRepository<Playlist> playlistRepository;

        public PlayerBL(IRepository<Category> categoryRepository,
                        IRepository<PlayableContent> contentRepository,
                        IRepository<Playlist> playlistRepository)
        {
            this.categoryRepository = categoryRepository;
            this.contentRepository = contentRepository;
            this.contentValidator = new PlayableContentValidator(this.contentRepository);
            this.playlistRepository = playlistRepository;
        }

        public List<Category> GetCategories()
        {
            return categoryRepository.GetAll().ToList();
        }

        public List<object> GetCategoryElements(int categoryId)
        {
            if(0 < categoryId && categoryId < 5)
            {
                var playlists = playlistRepository.GetAll().ToList();
                var contents = contentRepository.GetAll().ToList();
                List<object> auxReturn = new List<object>();

                foreach (Playlist p in playlists)
                {
                    if (p.Category.Id == categoryId) auxReturn.Add(p);
                }

                foreach (PlayableContent c in contents)
                {
                    if (c.Category.Id == categoryId) auxReturn.Add(c);
                }

                return auxReturn;
            }

            throw new Exception("There is no category associated to given id.");
        }

        public Playlist GetPlaylist(int playlistId)
        {
            if (playlistId > 0) 
            {
                return playlistRepository.Get(playlistId);
            }
            else
            {
                throw new Exception("No playlist associated to given id.");
            }
        }

        public PlayableContent GetPlayableContent(int contentId)
        {
            if (contentValidator.IdInValidRange(contentId))
            {
                return contentRepository.Get(contentId);
            }
            else throw new Exception("No content associated to given id.");
        }

        public PlayableContent AddIndependentContent (PlayableContent content)
        {
            contentValidator.ValidateContent(content);
            contentRepository.Add(content);
            return contentRepository.GetAll().ToList().FindLast(x => x.Name != null);
        }

        public void DeleteContent (int contentId)
        {
            if (contentValidator.IdInValidRange(contentId))
            {
                contentRepository.Delete(contentId);
            }
            else throw new Exception("No content associated to given id.");
        }

        public void ValidId(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid id");
            }
        }

        public void ValidatePlaylist (Playlist playlist)
        {
            foreach (Playlist auxPlaylist in playlistRepository.GetAll().ToList())
            {
                if (playlist.Name == auxPlaylist.Name && playlist.Description == auxPlaylist.Description)
                {
                    throw new Exception($"The playlist you are trying to create already exists at index {auxPlaylist.Id}");
                }
            }
        }

        public void AddPlaylist (Playlist playlist)
        {


            ValidatePlaylist(playlist);
            ValidId(playlist.CategoryId);
            playlistRepository.Add(playlist);
        }

        public void AlreadyOnPlaylist(int playlistId, int contentId)
        {
            Playlist auxPlaylist = playlistRepository.Get(playlistId);
            PlayableContent auxContent = contentRepository.Get(contentId);

            foreach (PlayableContent content in auxPlaylist.Contents)
            {
                if (content.Name == auxContent.Name && content.Author == auxContent.Author )
                {
                    throw new Exception($"The playable content you are trying to create already exists");
                }

            }
        }

        public void PlaylistExists(int playlistId)
        {
            Playlist auxPlaylist = playlistRepository.Get(playlistId);

            if (!playlistRepository.GetAll().ToList().Contains(auxPlaylist))
            {

                throw new Exception("There's no playlist associated to given id");
            }
        }

        public void ContentExists(int contentId)
        {
            PlayableContent auxContent = contentRepository.Get(contentId);

            if (auxContent == null)
            {
                throw new Exception("There's no playable content associated to given id");
            }
        }

        public Playlist AddContentToPlaylist(int playlistId, int contentId)
        {
            Playlist playlist = playlistRepository.Get(playlistId);
            PlayableContent content = contentRepository.Get(contentId);

            ContentExists(contentId);
            PlaylistExists(playlistId);
            AlreadyOnPlaylist(playlistId, contentId);

            playlist.Contents.Add(content);

            playlistRepository.Update(playlistId, playlist);

            return playlist;
        }

        public void RemoveContentsFromPlaylist(int playlistId)
        {
            Playlist playlist = GetPlaylist(playlistId);

            if (playlist.Contents.ToList().Count != 0)
            {
                foreach (PlayableContent content in playlist.Contents)
                {
                    playlist.Contents.ToList().Remove(content);
                }
            }
        }

        public void DeletePlaylist(int playlistId)
        {
            if (playlistId <= playlistRepository.GetAll().ToList().Count() && playlistId > 0)
            {
                playlistRepository.Delete(playlistId);
            }
            else throw new Exception("No playlist associated to given id");
        }

    }
        
}
