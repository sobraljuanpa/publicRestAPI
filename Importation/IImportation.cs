using Domain;
using System.Collections.Generic;

namespace IBusinessLogic
{
    public interface IImportation
    {
        public List<object> GetParameters();

        public PlayableContent GetPlayableContent();

        public VideoContent GetVideoContent();

        public List<PlayableContent> GetPlayableContents();

        public List<VideoContent> GetVideoContents();

        public Playlist GetPlaylist();

    }
}
