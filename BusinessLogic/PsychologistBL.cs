using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PsychologistBL : IPsychologistBL
    {
        private readonly IRepository<Psychologist> repository;

        public PsychologistBL(IRepository<Psychologist> psychologistRepository)
        {
            repository = psychologistRepository;
        }

        public Psychologist AddPsychologist(Psychologist psychologist)
        {
            repository.Add(psychologist);

            return repository.Get(psychologist.Id);
        }

        public void DeletePsychologist(int id)
        {
            repository.Delete(id);
        }

        public Psychologist GetPsychologist(int id)
        {
            return repository.Get(id);
        }

        public List<Psychologist> GetPsychologists()
        {
            return repository.GetAll().ToList();
        }

        public List<Psychologist> GetSpecialists(Problem problem)
        {
            var auxPsys = new List<Psychologist>();
            
            foreach(Psychologist psy in repository.GetAll().ToList())
            {
                if(psy.Expertise.Contains(problem))
                {
                    auxPsys.Add(psy);
                }
            }

            return auxPsys;
        }

        public void UpdatePsychologist(int id, Psychologist psychologist)
        {
            repository.Update(id, psychologist);
        }

        public void UpdateSchedule(int psychologistId, Schedule schedule)
        {
            var auxPsy = repository.Get(psychologistId);

            auxPsy.Schedule = schedule;

            repository.Update(psychologistId, auxPsy);
        }
    }
}
