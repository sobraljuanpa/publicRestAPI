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

        public bool IdInValidRange (int id)
        {
            return id > 0 && 
                   id <= contentRepository.GetAll().ToList()
                        .FindLast(x => x.Id != 0).Id;
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
    }
}