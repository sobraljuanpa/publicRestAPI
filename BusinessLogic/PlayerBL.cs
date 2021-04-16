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
            var playlists = playlistRepository.GetAll().ToList();
            var contents = contentRepository.GetAll().ToList();
            List<CategoryElement> auxReturn = new List<CategoryElement>();

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
            if (contentId <= contentRepository.GetAll().ToList().Count() && contentId > 0)
            {
                return contentRepository.Get(contentId);
            }
            else throw new Exception("No content associated to given id.");
        }

        public void ValidateContent (PlayableContent content)
        {
            if(content.Id != 0)
            {
                throw new Exception("Do not hardcode contentId, it is an autogenerated value.");
            }
            foreach (PlayableContent auxContent in contentRepository.GetAll().ToList())
            {
                if (content.Name == auxContent.Name &&  content.Author == auxContent.Author)
                {

                    throw new Exception($"The content you are trying to create already exists at index {auxContent.Id}");
                }
            }
        }

        public PlayableContent AddIndependentContent (PlayableContent content)
        {
            ValidateContent(content);
            contentRepository.Add(content);
            return contentRepository.Get(content.Id);
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

            return playlist;
        }

        public void DeleteContent (int contentId)
        {
            if (contentId <= contentRepository.GetAll().ToList().FindLast(x => x.Id != 0).Id && contentId > 0)
            {
                contentRepository.Delete(contentId);
            }
            else throw new Exception("No content associated to given id.");
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
