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
        VideoContent videoContent = null;

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
            catch(Exception)
            {
                throw new Exception("Incompatible format");
            }
        }

        public void AddVideoContent()
        {
            try
            {
                videoContent = _importation.GetVideoContent();
                _playerBL.AddVideoContent(videoContent);
            }
            catch (Exception)
            {
                throw new Exception("Incompatible format");
            }
        }

        public void AddPlaylist()
        {
            try
            {
                playlist = _importation.GetPlaylist();
                _playerBL.AddPlaylist(playlist);
                if (playlist.Contents.Count != 0)
                {
                    List<PlayableContent> playableContents = _importation.GetPlayableContents();
                    foreach (PlayableContent content in playableContents)
                    {
                        if (_playerBL.GetPlayableContent(content.Id) != null)
                        {
                            _playerBL.AddIndependentContent(content);
                        }
                        _playerBL.AddContentToPlaylist(playlist.Id, content.Id);
                    }
          
                }
            }
            catch(Exception)
            {
                throw new Exception("Incompatible format");
            }
        }

    }
}
