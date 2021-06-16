using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IContentBL
    {
        public PlayableContent GetPlayableContent(int id);
        public List<PlayableContent> GetContents();
        public PlayableContent AddIndependentContent(PlayableContent playableContent);
        public VideoContent AddVideoContent(VideoContent video);
        public VideoContent GetVideo(int id);
        public List<VideoContent> GetVideos();
        public void DeleteVideo(int id);
        public void DeleteContent(int id);
    }
}
