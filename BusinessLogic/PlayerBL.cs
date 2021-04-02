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

        private readonly IRepository<Playlist> playlistRepository;

        public PlayerBL(IRepository<Category> categoryRepository,
                        IRepository<PlayableContent> contentRepository,
                        IRepository<Playlist> playlistRepository)
        {
            this.categoryRepository = categoryRepository;
            this.contentRepository = contentRepository;
            this.playlistRepository = playlistRepository;
        }

        public List<Category> GetCategories()
        {
            return categoryRepository.GetAll().ToList();
        }

        public List<CategoryElement> GetCategoryElements(int categoryId)
        {
            return null;
        }

        public Playlist GetPlaylist(int playlistId)
        {
            return playlistRepository.Get(playlistId);

        }

        public PlayableContent GetPlayableContent(int contentId)
        {
            return null;
        }

        public void ValidationContent (int contentId)
        {

            PlayableContent exists = contentRepository.Get(contentId);
            if (exists != null)
            {
                throw new Exception("The playable content inserted already exists.");
            }
        }

        public void AddIndependentContent (PlayableContent content)
        {
            ValidationContent(content.Id);
            contentRepository.Add(content);
        }

        public void AddPlaylist (Playlist playlist)
        {

        }

        public void AlreadyOnPlaylist (Playlist playlist, PlayableContent content)
        {
            if (playlist.Contents.Contains(content))
            {
                throw new Exception("The playable content inserted already exists in the playlist.");
            }
        }

        public void ExitsPlaylist(Playlist playlist)
        {
            
            if (!playlistRepository.GetAll().ToList().Contains(playlist))
            {
                AddPlaylist(playlist);
            }
        }

        public void ExistsContent(PlayableContent content)
        {
            PlayableContent auxContent = contentRepository.Get(content.Id);
            if (auxContent == null)
            {
                contentRepository.Add(auxContent);
            } 
        }

        public void AddContentToPlaylist (Playlist playlist, PlayableContent content)
        {
            ExistsContent(content);
            ExitsPlaylist(playlist);
            AlreadyOnPlaylist(playlist,content);
            Playlist auxPlaylist = playlistRepository.Get(playlist.Id);
            PlayableContent auxContent = contentRepository.Get(content.Id);
            auxPlaylist.Contents.Add(auxContent);
        }

        public void DeleteContent (int contentId)
        {

        }

    }
        
}
