﻿using Domain;
using IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace BusinessLogic
{
    public class ImporterJSON : IImportation
    {
        public string _path;
        public PlayableContent playableContent = null;
        public Playlist playlist = null;

        public void ImporterJson(string path)
        {
            _path = path;
        }

        public PlayableContent PlayableContentRoot()
        {
            try
            {
                if (playableContent == null)
                {
                    PlayableContent contentRoot;
                    using (System.IO.StreamReader jsonStream = System.IO.File.OpenText(_path))
                    {
                        var json = jsonStream.ReadToEnd();
                        contentRoot = JsonConvert.DeserializeObject<PlayableContent>(json);
                    }

                    playableContent = contentRoot;

                    return contentRoot;
                }else
                {
                    return playableContent;
                }
            }
            catch (Exception e)
            {
                throw new Exception("..."+ e.Message);
            }
        }

        private Playlist playlistRoot()
        {
            try
            {
                if (playlist == null)
                {
                    Playlist playlistRoot;
                    using (System.IO.StreamReader jsonStream = System.IO.File.OpenText(_path))
                    {
                        var json = jsonStream.ReadToEnd();
                        playlistRoot = JsonConvert.DeserializeObject<Playlist>(json);
                    }

                    playlist = playlistRoot;

                    return playlistRoot;
                }
                else
                {
                    return playlist;
                }
            }
            catch (Exception e)
            {
                throw new Exception("..." + e.Message);
            }
        }

        public PlayableContent GetPlayableContent()
        {
            PlayableContent root = PlayableContentRoot();
            playableContent = root;

            return root;
        }

        public void GiveMePlayableContents(Playlist auxplaylist, List<PlayableContent> contents)
        {
            if (auxplaylist.Contents != null)
            {
                foreach (var content in auxplaylist.Contents)
                {
                    contents.Add(content);
                }
            }
        }

        public List<PlayableContent> GetPlayableContents()
        {
            List<PlayableContent> contentList = new List<PlayableContent>();
            Playlist root = playlistRoot();
            GiveMePlayableContents(root, contentList);

            return contentList;
        }

        public Playlist GetPlaylist()
        {
            Playlist root = playlistRoot();
            playlist = root;

            return root;
        }

        enum Parameter
        {
            FILE,
            STRING,
            INTEGER,
            DATE
        }

        public List<object> GetParameters()
        {
            List<object> parameters = new List<object>();
            parameters.Add(Parameter.FILE);

            return parameters;
        }
    }
}
