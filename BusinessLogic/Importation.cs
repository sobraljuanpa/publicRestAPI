using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class Importation : IImportationBL
    {
        IImportation _importation;
        IPlayerBL _playerBL;
        Playlist playlist = null;
        List<PlayableContent> playableContents = null;
        List<VideoContent> videoContents = null;

        public Importation(IImportation importation, IPlayerBL playerBL)
        {
            _importation = importation;
            _playerBL = playerBL;
        }

        public void AddPlayableContents()
        {
            try
            {
                playableContents = _importation.GetPlayableContents();

                foreach (PlayableContent playableContent in playableContents)
                {
                    _playerBL.AddIndependentContent(playableContent);
                }
                
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
                videoContents = _importation.GetVideoContents();

                foreach (VideoContent videoContent in videoContents)
                {
                    _playerBL.AddVideoContent(videoContent);
                }

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
