using Domain;
using IBusinessLogic;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BusinessLogic
{
    public class ImporterJSON : IImportation
    {
        public string _path;
        public PlayableContent playableContent = null;
        public Playlist playlist = null;
        public VideoContent videoContent = null;

        public ImporterJSON (string path)
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
            catch (Exception)
            {
                throw new Exception("Not possible to deserialize JSON file");
            }
        }

        public VideoContent VideoContentRoot()
        {
            try
            {
                if (videoContent == null)
                {
                    VideoContent contentRoot;
                    using (System.IO.StreamReader jsonStream = System.IO.File.OpenText(_path))
                    {
                        var json = jsonStream.ReadToEnd();
                        contentRoot = JsonConvert.DeserializeObject<VideoContent>(json);
                    }

                    videoContent = contentRoot;

                    return contentRoot;
                }
                else
                {
                    return videoContent;
                }
            }
            catch (Exception)
            {
                throw new Exception("Not possible to deserialize JSON file");
            }
        }

        private Playlist PlaylistRoot()
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
            catch (Exception)
            {
                throw new Exception("Not possible to deserialize JSON file" );
            }
        }

        public void GetContentsFromPlaylist(Playlist auxplaylist, List<PlayableContent> contents)
        {
            if (auxplaylist.Contents != null)
            {
                foreach (var content in auxplaylist.Contents)
                {
                    contents.Add(content);
                }
            }
        }

        public void GetVideoContentsFromPlaylist(Playlist auxplaylist, List<VideoContent> videos)
        {
            if (auxplaylist.Videos != null)
            {
                foreach (var video in auxplaylist.Videos)
                {
                    videos.Add(video);
                }
            }
        }

        public List<VideoContent> GetVideoContents()
        {
            List<VideoContent> videoList = new List<VideoContent>();
            Playlist root = PlaylistRoot();
            GetVideoContentsFromPlaylist(root, videoList);

            return videoList;
        }

        public List<PlayableContent> GetPlayableContents()
        {
            List<PlayableContent> contentList = new List<PlayableContent>();
            Playlist root = PlaylistRoot();
            GetContentsFromPlaylist(root, contentList);

            return contentList;
        }

        public PlayableContent GetPlayableContent()
        {
            PlayableContent root = PlayableContentRoot();

            return root;
        }

        public VideoContent GetVideoContent()
        {
            VideoContent root = VideoContentRoot();

            return root;
        }

        public Playlist GetPlaylist()
        {
            Playlist root = PlaylistRoot();

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
            parameters.Add(Parameter.STRING);

            return parameters;
        }
    }
}
