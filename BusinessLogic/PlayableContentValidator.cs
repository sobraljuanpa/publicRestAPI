using System.Linq;
using System;

using IDataAccess;
using Domain;

namespace BusinessLogic
{
    public class PlayableContentValidator
    {
        private readonly IRepository<PlayableContent> contentRepository;
        
        public PlayableContentValidator(IRepository<PlayableContent> repository)
        {
            contentRepository = repository;
        }

        public void IdInValidRange (int id)
        {
            if(id <= 0 || id > contentRepository.GetAll().Count())
            {
                throw new Exception("No content associated to given id");
            }
        }

        public void ValidateContent (PlayableContent content)
        {
            if(content.Id != 0)
            {
                throw new Exception("Do not hardcode contentId, it is an autogenerated value.");
            }
            foreach (PlayableContent auxContent in contentRepository.GetAll().ToList())
            {
                if (content.Name == auxContent.Name &&  content.Author == auxContent.Author)
                {

                    throw new Exception($"The content you are trying to create already exists at index {auxContent.Id}");
                }
            }
        }

        public void Exists (PlayableContent content)
        {
            if (content == null)
            {
                throw new Exception("There's no playable content associated to given id");
            }
        }
    }
}