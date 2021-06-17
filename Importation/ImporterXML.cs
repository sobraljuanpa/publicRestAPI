using Domain;
using IBusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BusinessLogic
{
    public class ImporterXML : IImportation
    {
        public string _path;
        public PlayableContent playableContent = null;
        public VideoContent videoContent = null;
        public Playlist playlist = null;

        public ImporterXML(string path)
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
                    XmlSerializer serializer = new XmlSerializer(typeof(PlayableContent));
                    System.IO.StreamReader reader = new System.IO.StreamReader(_path);
                    contentRoot = (PlayableContent)serializer.Deserialize(reader);
                    playableContent = contentRoot;

                    return contentRoot;
                }
                else
                {
                    return playableContent;
                }
            }
            catch (Exception)
            {
                throw new Exception("Not possible to deserialize Xml file");
            }
        }

        public VideoContent VideoContentRoot()
        {
            try
            {
                if (videoContent == null)
                {
                    VideoContent videoRoot;
                    XmlSerializer serializer = new XmlSerializer(typeof(VideoContent));
                    System.IO.StreamReader reader = new System.IO.StreamReader(_path);
                    videoRoot = (VideoContent)serializer.Deserialize(reader);
                    videoContent = videoRoot;

                    return videoRoot;
                }
                else
                {
                    return videoContent;
                }
            }
            catch (Exception)
            {
                throw new Exception("Not possible to deserialize Xml file");
            }
        }

        private Playlist PlaylistRoot()
        {
            try
            {
                if (playlist == null)
                {
                    Playlist playlistRoot;
                    XmlSerializer serializer = new XmlSerializer(typeof(Playlist));
                    System.IO.StreamReader reader = new System.IO.StreamReader(_path);
                    playlistRoot = (Playlist)serializer.Deserialize(reader);
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
                throw new Exception("Not possible to deserialize Xml file");
            }

        }

        public PlayableContent GetPlayableContent()
        {
            PlayableContent root = PlayableContentRoot();
            playableContent = root;

            return root;
        }


        public VideoContent GetVideoContent()
        {
            VideoContent root = VideoContentRoot();
            videoContent = root;

            return root;
        }

        public void GetContentsFromPlaylis(Playlist auxplaylist, List<PlayableContent> contents)
        {
            if (auxplaylist.Contents != null)
            {
                foreach (var content in auxplaylist.Contents)
                {
                    contents.Add(content);
                }
            }
        }

        public void GetVideosFromPlaylis(Playlist auxplaylist, List<VideoContent> contents)
        {
            if (auxplaylist.Videos != null)
            {
                foreach (var video in auxplaylist.Videos)
                {
                    contents.Add(video);
                }
            }
        }

        public List<PlayableContent> GetPlayableContents()
        {
            List<PlayableContent> contentList = new List<PlayableContent>();
            Playlist root = PlaylistRoot();
            GetContentsFromPlaylis(root, contentList);

            return contentList;
        }

        public List<VideoContent> GetVideoContents()
        {
            List<VideoContent> videoList = new List<VideoContent>();
            Playlist root = PlaylistRoot();
            GetVideosFromPlaylis(root, videoList);

            return videoList;
        }

        public Playlist GetPlaylist()
        {
            Playlist root = PlaylistRoot();
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
            parameters.Add(Parameter.STRING);

            return parameters;
        }
    }
}
