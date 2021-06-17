using Domain;
using System.Collections.Generic;

namespace IBusinessLogic
{
    public interface IImportation
    {

        public List<PlayableContent> GetPlayableContents();

        public List<VideoContent> GetVideoContents();

        public Playlist GetPlaylist();

    }
}
