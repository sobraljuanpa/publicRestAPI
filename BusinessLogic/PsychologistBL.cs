using Domain;
using Domain.DTOs;
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

        public Schedule CreateEmptySchedule()
        {
            Schedule schedule = new Schedule
            {

                MondayConsultations = 0,
                TuesdayConsultations = 0,
                WednesdayConsultations = 0,
                ThursdayConsultations = 0,
                FridayConsultations = 0
            };

            AddSchedule(schedule);

            return schedule;
        }

        public Psychologist ToEntity(PsychologistDTO dto)
        {
            var problem1 = problemRepository.Get(dto.ExpertiseId1);
            var problem2 = problemRepository.Get(dto.ExpertiseId2);
            var problem3 = problemRepository.Get(dto.ExpertiseId3);
            
            Psychologist aux = new Psychologist
            {
                PsychologistName = dto.PsychologistName,
                PsychologistSurname = dto.PsychologistSurname,
                IsRemote = dto.IsRemote,
                Address = dto.Address,
                ActiveYears = dto.ActiveYears,
                Expertise = new List<Problem> { problem1, problem2, problem3 }
            };

            return aux;
        }

        public Psychologist AddPsychologist(PsychologistDTO psychologist)
        {
            Psychologist psy = ToEntity(psychologist);

            Schedule schedule = CreateEmptySchedule();

            psychologist.ScheduleId = schedule.Id;

            repository.Add(psy);

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
