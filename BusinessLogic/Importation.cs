using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Importation : IImportationLogic
    {
        IImportation _importation;
        PlayerBL _playerBL;
        Playlist playlist = null;
        PlayableContent playableContent = null;

        public Importation(IImportation importation, PlayerBL playerBL)
        {
            _importation = importation;
            _playerBL = playerBL;
        }

        public void AddPlayableContent()
        {
            try
            {
                playableContent = _importation.GetPlayableContent();
                _playerBL.AddIndependentContent(playableContent);
            }
            catch(Exception e)
            {
                throw new Exception("..");
            }
        }

        public void AddPlaylist()
        {
            try
            {
                playlist = _importation.GetPlaylist();
                _playerBL.AddPlaylist(playlist);
            }
            catch(Exception e)
            {
                throw new Exception("..");
            }
        }

    }
}
