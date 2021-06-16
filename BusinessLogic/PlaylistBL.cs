using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PlaylistBL : IPlaylistBL
    {
        private readonly IRepository<PlayableContent> contentRepository;

        private readonly PlayableContentValidator contentValidator;

        private readonly IRepository<Playlist> playlistRepository;

        private readonly PlaylistValidator playlistValidator;

        private readonly IRepository<VideoContent> videosRepository;

        public PlaylistBL(IRepository<PlayableContent> contentRepo, IRepository<VideoContent> videoRepo, IRepository<Playlist> playlistRepo)
        {
            contentRepository = contentRepo;
            videosRepository = videoRepo;
            playlistRepository = playlistRepo;

            playlistValidator = new PlaylistValidator(playlistRepository);
            contentValidator = new PlayableContentValidator(contentRepository);
        }

        public Playlist GetPlaylist(int id)
        {
            playlistValidator.IdInValidRange(id);

            return playlistRepository.Get(id);
        }

        public List<PlayableContent> GetPlaylistContents(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);

            return playlistRepository.Get(playlistId).Contents.ToList();
        }

        public List<VideoContent> GetPlaylistVideos(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);

            return playlistRepository.Get(playlistId).Videos.ToList();
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

        public void AddPlaylist(Playlist playlist)
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

        public Playlist AddVideoToPlaylist(int playlistId, int videoId)
        {
            Playlist playlist = playlistRepository.Get(playlistId);
            VideoContent content = videosRepository.Get(videoId);

            if (playlist.Videos == null) playlist.Videos = new List<VideoContent> { };

            if (!playlist.Videos.Contains(content))
            {
                playlist.Videos.Add(content);

                playlistRepository.Update(playlistId, playlist);
            }

            return playlist;
        }

        public void DeleteVideoFromPlaylist(int playlistId, int videoId)
        {
            Playlist playlist = playlistRepository.Get(playlistId);
            VideoContent video = videosRepository.Get(videoId);

            playlist.Videos.Remove(video);

            playlistRepository.Update(playlistId, playlist);
        }

        public void DeletePlaylist(int playlistId)
        {
            playlistValidator.IdInValidRange(playlistId);
            playlistRepository.Delete(playlistId);
        }

    }

}
