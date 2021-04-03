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

        public void ValidateContent (PlayableContent content)
        {

            foreach (PlayableContent auxContent in contentRepository.GetAll().ToList())
            {
                if (content.Name == auxContent.Name && content.ImageURL == auxContent.ImageURL && 
                    content.Duration == auxContent.Duration && content.ContentURL == auxContent.ContentURL 
                    && content.Category == auxContent.Category && content.Author == auxContent.Author)
                {

                    throw new Exception("The playable content inserted already exists.");
                }
            }
        }

        public void AddIndependentContent (PlayableContent content)
        {
            ValidateContent(content);
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

        public void DeleteContent (int contentId)
        {

        }

    }
        
}
