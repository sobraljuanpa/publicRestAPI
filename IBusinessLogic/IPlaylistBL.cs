using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IPlaylistBL
    {
        public Playlist GetPlaylist(int id);
        public List<PlayableContent> GetPlaylistContents(int playlistId);
        public List<VideoContent> GetPlaylistVideos(int playlistId);
        public List<Playlist> GetPlaylists();
        public void AddPlaylist(Playlist playlist);
        public Playlist AddContentToPlaylist(int playlistId, int contentId);
        public void DeleteContentFromPlaylist(int playlistId, int contentId);
        public Playlist AddVideoToPlaylist(int playlistId, int videoId);
        public void DeleteVideoFromPlaylist(int playlistId, int videoId);
        public void DeletePlaylist(int playlistId);
    }
}
