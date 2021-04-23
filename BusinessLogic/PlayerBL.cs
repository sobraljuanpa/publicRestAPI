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

        private readonly PlaylistValidator playlistValidator;

        public PlayerBL(IRepository<Category> categoryRepository,
                        IRepository<PlayableContent> contentRepository,
                        IRepository<Playlist> playlistRepository)
        {
            this.categoryRepository = categoryRepository;
            this.contentRepository = contentRepository;
            this.contentValidator = new PlayableContentValidator(this.contentRepository);
            this.playlistRepository = playlistRepository;
            this.playlistValidator = new PlaylistValidator(this.playlistRepository);
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
            playlistValidator.IdInValidRange(playlistId);

            return playlistRepository.Get(playlistId);
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

        public void AddPlaylist (Playlist playlist)
        {

            playlistValidator.ValidPlaylist(playlist);
            //playlistValidator.IdInValidRange(playlist.Id);

            playlistRepository.Add(playlist);
        } 

        public Playlist AddContentToPlaylist(int playlistId, int contentId)
        {
            Playlist playlist = playlistRepository.Get(playlistId);
            PlayableContent content = contentRepository.Get(contentId);

            contentValidator.Exists(content);
            playlistValidator.Exists(playlist);
            playlistValidator.AlreadyOnPlaylist(playlist, content);

            playlist.Contents.Add(content);
            //TODO 
            //ACA NO SE ESTA GUARDANDO NUNCA A BD EL CAMBIO, OJO

            return playlist;
        }

        public void DeletePlaylist(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);
            playlistRepository.Delete(playlistId);
        }

    }
        
}
