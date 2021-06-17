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

        private readonly IPlaylistBL PlaylistBL;

        private readonly IContentBL ContentBL;

        public PlayerBL(IRepository<Category> categoryRepository,
                        IRepository<PlayableContent> contentRepository,
                        IRepository<Playlist> playlistRepository,
                        IRepository<VideoContent> videosRepository)
        {
            this.categoryRepository = categoryRepository;

            ContentBL = new ContentBL(contentRepository, videosRepository);
            PlaylistBL = new PlaylistBL(contentRepository, videosRepository, playlistRepository);
        }

        public List<Category> GetCategories()
        {
            return categoryRepository.GetAll().ToList();
        }

        public List<object> GetCategoryElements(int categoryId)
        {
            if(0 < categoryId && categoryId < 5)
            {

                List<object> auxReturn = new List<object>();

                var playlists = PlaylistBL.GetPlaylists();
                foreach (Playlist p in playlists)
                {
                    if (p.Category.Id == categoryId) auxReturn.Add(p);
                }

                var contents = ContentBL.GetContents();
                foreach (PlayableContent c in contents)
                {
                    if (c.CategoryId == categoryId) auxReturn.Add(c);
                }

                var videos = ContentBL.GetVideos();
                foreach (VideoContent v in videos)
                {
                    if (v.CategoryId == categoryId) auxReturn.Add(v);
                }

                return auxReturn;
            }

            throw new NullReferenceException("There is no category associated to given id.");
        }

        public Playlist GetPlaylist(int playlistId)
        {
            return PlaylistBL.GetPlaylist(playlistId);
        }

        public List<PlayableContent> GetPlaylistContents(int playlistId)
        {
            return PlaylistBL.GetPlaylistContents(playlistId);
        }

        public List<VideoContent> GetPlaylistVideos(int playlistId)
        {
            return PlaylistBL.GetPlaylistVideos(playlistId);
        }

        public List<Playlist> GetPlaylists()
        {
            return PlaylistBL.GetPlaylists();
        }

        public PlayableContent GetPlayableContent(int contentId)
        {
            return ContentBL.GetPlayableContent(contentId);
        }

        public List<PlayableContent> GetContents()
        {
            return ContentBL.GetContents();
        }

        public VideoContent GetVideo(int videoId)
        {
            return ContentBL.GetVideo(videoId);
        }

        public List<VideoContent> GetVideos()
        {
            return ContentBL.GetVideos();
        }

        public void DeleteVideo(int id)
        {
            ContentBL.DeleteVideo(id);
        }

        public PlayableContent AddIndependentContent (PlayableContent content)
        {
            return ContentBL.AddIndependentContent(content);
        }

        public VideoContent AddVideoContent (VideoContent video)
        {
            return ContentBL.AddVideoContent(video);
        }

        public void DeleteContent (int contentId)
        {
            ContentBL.DeleteContent(contentId);
        }

        public void AddPlaylist (Playlist playlist)
        {
            PlaylistBL.AddPlaylist(playlist);
        } 

        public Playlist AddContentToPlaylist(int playlistId, int contentId)
        {
            return PlaylistBL.AddContentToPlaylist(playlistId, contentId);
        }

        public void DeleteContentFromPlaylist(int playlistId, int contentId)
        {
            PlaylistBL.DeleteContentFromPlaylist(playlistId, contentId);
        }

        public Playlist AddVideoToPlaylist(int playlistId, int videoId)
        {
            return PlaylistBL.AddVideoToPlaylist(playlistId, videoId);
        }

        public void DeleteVideoFromPlaylist(int playlistId, int videoId)
        {
            PlaylistBL.DeleteVideoFromPlaylist(playlistId, videoId);
        }

        public void DeletePlaylist(int playlistId)
        {
            PlaylistBL.DeletePlaylist(playlistId);
        }

    }
        
}
