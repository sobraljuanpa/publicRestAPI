using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ContentBL : IContentBL
    {

        private readonly IRepository<PlayableContent> contentRepository;

        private readonly PlayableContentValidator contentValidator;

        private readonly IRepository<VideoContent> videosRepository;

        public ContentBL(IRepository<PlayableContent> contentRepo, IRepository<VideoContent> videosRepo)
        {
            contentRepository = contentRepo;
            videosRepository = videosRepo;

            contentValidator = new PlayableContentValidator(contentRepo);
        }

        public PlayableContent GetPlayableContent(int contentId)
        {
            contentValidator.IdInValidRange(contentId);

            return contentRepository.Get(contentId);
        }

        public List<PlayableContent> GetContents()
        {
            var contents = contentRepository.GetAll();

            foreach (PlayableContent c in contents)
            {
                c.Playlists = null;
            }

            return contents.ToList();
        }

        public VideoContent GetVideo(int videoId)
        {
            return videosRepository.Get(videoId);
        }

        public List<VideoContent> GetVideos()
        {
            var videos = videosRepository.GetAll();

            foreach (VideoContent v in videos)
            {
                v.Playlists = null;
            }

            return videos.ToList();
        }

        public void DeleteVideo(int id)
        {
            videosRepository.Delete(id);
        }

        public PlayableContent AddIndependentContent(PlayableContent content)
        {
            contentValidator.ValidateContent(content);
            contentRepository.Add(content);

            return contentRepository.GetAll().ToList().FindLast(x => x.Name != null);
        }

        public VideoContent AddVideoContent(VideoContent video)
        {
            videosRepository.Add(video);

            return videosRepository.GetAll().ToList().FindLast(x => x.Name != null);
        }

        public void DeleteContent(int contentId)
        {
            contentValidator.IdInValidRange(contentId);
            contentRepository.Delete(contentId);
        }

    }

}
