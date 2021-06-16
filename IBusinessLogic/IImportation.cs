using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBusinessLogic
{
    public interface IImportation
    {
        //public String GetImportationName();

        public List<object> GetParameters();

        public PlayableContent GetPlayableContent();

        public VideoContent GetVideoContent();

        public List<PlayableContent> GetPlayableContents();

        public List<VideoContent> GetVideoContents();

        public Playlist GetPlaylist();

        //public List<object> ImportContent(List<object> parameters);

    }
}
