using System;
using System.Collections.Generic;
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
            return null;
        }

        public List<CategoryElement> GetCategoryElements(int categoryId)
        {
            return null;
        }

        public Playlist GetPlaylist(int playlistId)
        {
            return null;

        }

        public PlayableContent GetPlayableContent(int contentId)
        {
            return null;
        }

        public void AddIndependentContent (PlayableContent playableContent)
        {
           
        }

        public void AddPlaylist (Playlist playlist)
        {

        }

        public void AddContentToPlaylist (int playlistId, int contentId)
        {

        }

        public void DeleteContent (int contentId)
        {

        }

    }
        
}
