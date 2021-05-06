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

        private readonly IRepository<Problem> problemRepository;

        private readonly IRepository<Schedule> scheduleRepository;

        public PsychologistBL(IRepository<Psychologist> psychologistRepository,
                              IRepository<Problem> repositoryProblem,
                              IRepository<Schedule> repositorySchedule)
        {
            repository = psychologistRepository;
            problemRepository = repositoryProblem;
            scheduleRepository = repositorySchedule;
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

        public void AlreadyOnList(Psychologist psychologist, int problemId)
        {
            foreach (Problem problem in psychologist.Expertise)
            {
                if(problem.Id == problemId)
                {
                    throw new Exception("The expertise your trying to add already exists.");
                }
            }
        }

        public Psychologist AddProblemToPsychologist(Psychologist psychologist, int problemId)
        {
            Problem problem = problemRepository.Get(problemId);
            AlreadyOnList(psychologist, problemId);

            psychologist.Expertise.Add(problem);

            repository.Update(psychologist.Id,psychologist);

            return psychologist;
        }

        public void ValidSchedule(Schedule schedule)
        {
            if(schedule == null)
            {
                throw new Exception("The schedule you are trying to add is invalid.");
            }
        }

        public Schedule AddSchedule(Schedule schedule)
        {
            ExistsSchedule(schedule.Id);
            scheduleRepository.Add(schedule);

            return scheduleRepository.Get(schedule.Id);
        }

        public void ExistsSchedule(int scheduleId)
        {
            foreach (Schedule auxSchedule in scheduleRepository.GetAll().ToList())
            {
                if (auxSchedule.Id == scheduleId)
                {
                    throw new Exception($"The schedule you are trying to add already exists at index {auxSchedule.Id}");
                }
            }
        }
        public Psychologist AddScheduleToPsychologist(Psychologist psychologist, int id)
        {
            Psychologist auxPsychologist = repository.Get(psychologist.Id);
            Schedule schedule = scheduleRepository.Get(id);

            ValidSchedule(schedule);

            auxPsychologist.Schedule = schedule;
            repository.Update(auxPsychologist.Id, auxPsychologist);

            return repository.Get(auxPsychologist.Id);
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

        public Schedule GetSchedule(int id)
        {
            return scheduleRepository.Get(id);
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
