﻿using System;
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

        private readonly IRepository<VideoContent> videosRepository;

        public PlayerBL(IRepository<Category> categoryRepository,
                        IRepository<PlayableContent> contentRepository,
                        IRepository<Playlist> playlistRepository,
                        IRepository<VideoContent> videosRepository)
        {
            this.categoryRepository = categoryRepository;
            this.contentRepository = contentRepository;
            this.contentValidator = new PlayableContentValidator(this.contentRepository);
            this.playlistRepository = playlistRepository;
            this.playlistValidator = new PlaylistValidator(this.playlistRepository);
            this.videosRepository = videosRepository;
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
                    if (c.CategoryId == categoryId) auxReturn.Add(c);
                }

                return auxReturn;
            }

            throw new NullReferenceException("There is no category associated to given id.");
        }

        public Playlist GetPlaylist(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);

            return playlistRepository.Get(playlistId);
        }

        public List<PlayableContent> GetPlaylistContents(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);

            return playlistRepository.Get(playlistId).Contents.ToList();
        }

        public List<Playlist> GetPlaylists()
        {
            var playlists = playlistRepository.GetAll();

            foreach (Playlist p in playlists)
            {
                foreach (PlayableContent c in p.Contents)
                {
                    c.Playlists = null;
                }
            }

            return playlists.ToList();
        }

        public PlayableContent GetPlayableContent(int contentId)
        {
            contentValidator.IdInValidRange(contentId);

            return contentRepository.Get(contentId);
        }

        public List<PlayableContent> GetContents()
        {
            var contents = contentRepository.GetAll();
            
            foreach(PlayableContent c in contents)
            {
                c.Playlists = null;
            }

            return contents.ToList();
        }

        public PlayableContent AddIndependentContent (PlayableContent content)
        {
            contentValidator.ValidateContent(content);
            contentRepository.Add(content);

            return contentRepository.GetAll().ToList().FindLast(x => x.Name != null);
        }

        public VideoContent AddVideoContent (VideoContent video)
        {
            videosRepository.Add(video);

            return videosRepository.GetAll().ToList().FindLast(x => x.Name != null);
        }

        public void DeleteContent (int contentId)
        {
            contentValidator.IdInValidRange(contentId);
            contentRepository.Delete(contentId);
        }

        public void AddPlaylist (Playlist playlist)
        {
            playlistValidator.ValidPlaylist(playlist);
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

            playlistRepository.Update(playlistId, playlist);

            return playlist;
        }

        public void DeleteContentFromPlaylist(int playlistId, int contentId)
        {
            Playlist playlist = playlistRepository.Get(playlistId);
            PlayableContent content = contentRepository.Get(contentId);

            playlist.Contents.Remove(content);

            playlistRepository.Update(playlistId, playlist);
        }

        public void DeletePlaylist(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);
            playlistRepository.Delete(playlistId);
        }

    }
        
}
