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
            return contentRepository.GetAll().ToList().FindLast(x => x.Id != 0);
        }

        public void DeleteContent (int contentId)
        {
            if (contentValidator.IdInValidRange(contentId))
            {
                contentRepository.Delete(contentId);
            }
            else throw new Exception("No content associated to given id.");
        }

        public void ExistsPlaylist(Playlist playlist)
        {
            Playlist p = GetPlaylist(playlist.Id);
            if (GetPlaylist(playlist.Id) != null)
            {
                throw new Exception("The playlist you are trying to create already exists.");
            }
        }

        public void ValidId(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid id");
            }
        }

        public void AddPlaylist (Playlist playlist)
        {
            //ExistsPlaylist(playlist);
            //ValidId(playlist.Id);
            //ValidId(playlist.CategoryId);
            playlistRepository.Add(playlist);
        }

        public void AlreadyOnPlaylist (Playlist playlist, PlayableContent content)
        {
            if (playlist.Contents.Contains(content))
            {
                throw new Exception("The playable content inserted already exists in the playlist.");
            }
        }

        public void PlaylistExists(Playlist playlist)
        {
            
            if (!playlistRepository.GetAll().ToList().Contains(playlist))
            {
                playlistRepository.Add(playlist);
            }
        }

        public void ContentExists(PlayableContent content)
        {
            PlayableContent auxContent = contentRepository.Get(content.Id);
            if (auxContent == null)
            {
                contentRepository.Add(auxContent);
            } 
        }

        public void SameCategory(Playlist playlist, PlayableContent content)
        {
            if (playlist.Category != content.Category)
            {
                throw new Exception("This playable content cannot be added to the playlist.");
            }
        }

        public void AddContentToPlaylist (Playlist playlist, PlayableContent content)
        {
            SameCategory(playlist, content);
            ContentExists(content);
            PlaylistExists(playlist);
            AlreadyOnPlaylist(playlist, content);
            //Playlist auxPlaylist = playlistRepository.Get(playlist.Id);
            //PlayableContent auxContent = contentRepository.Get(content.Id);
            playlist.Contents.Add(content);
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
            RemoveContentsFromPlaylist(playlistId);
            playlistRepository.Delete(playlistId);
        }

    }
        
}
