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

        private void ValidateId(int id)
        {
            if(id <= 0 || id > repository.GetAll().OrderBy(x => x.Id).Last().Id)
            {
                throw new Exception($"There is no psychologist associated to given id {id}.");
            }
        }

        public Psychologist AddPsychologist(Psychologist psychologist)
        {
            repository.Add(psychologist);
            return repository.Get(psychologist.Id);
        }

        public void DeletePsychologist(int id)
        {
            ValidateId(id);
            repository.Delete(id);
        }

        public Psychologist GetPsychologist(int id)
        {
            ValidateId(id);
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
            ValidateId(id);
            repository.Update(id, psychologist);
        }

        public void UpdateSchedule(int psychologistId, Schedule schedule)
        {
            ValidateId(psychologistId);

            var auxPsy = repository.Get(psychologistId);

            auxPsy.Schedule = schedule;

            repository.Update(psychologistId, auxPsy);
        }
    }
}
