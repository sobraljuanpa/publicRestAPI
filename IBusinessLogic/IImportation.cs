using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBusinessLogic
{
    public interface IImportation
    {
        public String GetImportationName();

        public List<object> GetParameters();

        public List<PlayableContent> GetPlayableContents();

        public List<Playlist> GetPlaylists();

        public List<object> ImportContent(List<object> parameters);

    }
}
