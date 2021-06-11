﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IPlayerBL
    {
        public List<Category> GetCategories();
        public List<object> GetCategoryElements(int id);
        public Playlist GetPlaylist(int id);
        public List<PlayableContent> GetPlaylistContents(int playlistId);
        public List<VideoContent> GetPlaylistVideos(int playlistId);
        public List<Playlist> GetPlaylists();
        public PlayableContent GetPlayableContent(int id);
        public List<PlayableContent> GetContents();
        public PlayableContent AddIndependentContent(PlayableContent playableContent);
        public VideoContent AddVideoContent(VideoContent video);
        public VideoContent GetVideo(int id);
        public List<VideoContent> GetVideos();
        public void DeleteVideo(int id);
        public void AddPlaylist(Playlist playlist);
        public Playlist AddContentToPlaylist(int playlistId, int contentId);
        public void DeleteContentFromPlaylist(int playlistId, int contentId);
        public Playlist AddVideoToPlaylist(int playlistId, int videoId);
        public void DeleteVideoFromPlaylist(int playlistId, int videoId);
        public void DeleteContent(int id);
        public void DeletePlaylist(int id);
    }
}
