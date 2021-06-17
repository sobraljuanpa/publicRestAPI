using System;
using System.Collections.Generic;
using System.Text;

namespace IBusinessLogic
{
    public interface IImportationBL
    {
        public void LoadFile(string type, object[] parameters);
        public void AddPlayableContent();

        public void AddVideoContent();

        public void AddPlaylist();
    }
}
