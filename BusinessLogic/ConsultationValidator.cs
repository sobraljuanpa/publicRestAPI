﻿using System.Linq;
using System;

using IDataAccess;
using Domain;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class ConsultationValidator
    {
        private readonly IRepository<Consultation> consultationRepository;
        private readonly IRepository<Psychologist> psychologistRepository;


        public ConsultationValidator(IRepository<Consultation> repositoryConsultation,
                                     IRepository<Psychologist> repositoryPsychologist)
        {
            consultationRepository = repositoryConsultation;
            psychologistRepository = repositoryPsychologist;

        }

        public void IdValidRange(int id)
        {
            if (id <= 0 || id > consultationRepository.GetAll().ToList().Count())
            {
                throw new Exception("No consultation associated to given id");
            }

        }

        public List<Consultation> FindConsultations(int id)
        {
            List<Consultation> consultations = new List<Consultation>();

            foreach (Consultation auxConsultation in consultationRepository.GetAll().ToList())
            {
                if (auxConsultation.Psychologist.Id == id)
                {
                    consultations.Add(auxConsultation);
                }
            }
            if (consultations.Count() == 0)
            {
                throw new Exception("No consultation associated to given psychologist");
            }

            return consultations;
        }

        public bool IsOnList(Psychologist psychologist, Problem problem)
        {
            bool ok = false;

            foreach (Problem auxProblem in psychologist.Expertise.ToList())
            {
                if (auxProblem.Id == problem.Id)
                {
                    ok = true;
                }
            }

            return ok;
        }

        public List<Psychologist> PsychologistsWithExpertise(Problem problem)
        {
            List<Psychologist> expertise = new List<Psychologist>();

            foreach (Psychologist auxPsychologist in psychologistRepository.GetAll().ToList())
            {
                if (IsOnList(auxPsychologist, problem))
                {
                    expertise.Add(auxPsychologist);
                }
            }

            return expertise;
        }

        public Psychologist GetExpert(List<Psychologist> experts)
        {
            Psychologist expert = new Psychologist();
            expert.ActiveYears = 0;

            foreach (Psychologist auxPsychologist in experts)
            {
                if (auxPsychologist.ActiveYears > expert.ActiveYears)
                {
                    expert = auxPsychologist;
                }
            }

            return expert;
        }

        public void AssignPsychologist(Consultation consultation)
        {
            List<Psychologist> experts = PsychologistsWithExpertise(consultation.Problem);
            consultation.Psychologist = GetExpert(experts);
        }

        public void IdValidRangePs(int id)
        {
            if (id <= 0 || id > psychologistRepository.GetAll().ToList().Count())
            {
                throw new Exception("No psychologist associated to given id");
            }
        }

        public void ValidSchedule(Psychologist psychologist)
        {
            if (psychologist.Schedule == null)
            {
                throw new Exception("Invalid schedule.");
            }
        }

        public void ValidRemoteAddress(String address)
        {
            string format = @"\A[https]+(\://)[betterCalm]+(\.)[com]+(\.)[uy]+(\/)[meeting_id]+(\/)[a-z0-9]";

            if (!Regex.Match(address, format).Success)
            {
                throw new Exception("Address with wrong format.");
            }
        }

        public void ValidAddress(Consultation consultation)
        {
            if (consultation.IsRemote)
            {
                ValidRemoteAddress(consultation.Address);
            }

        }

        public void FullSchedule(Consultation consultation)
        {
            if (consultation.Date == 0 && consultation.Psychologist.Schedule.MondayConsultations == 5
               || consultation.Date == 1 && consultation.Psychologist.Schedule.TuesdayConsultations == 5
               || consultation.Date == 2 && consultation.Psychologist.Schedule.WednesdayConsultations == 5
               || consultation.Date == 3 && consultation.Psychologist.Schedule.ThursdayConsultations == 5
               || consultation.Date == 4 && consultation.Psychologist.Schedule.FridayConsultations == 5)
            {
                throw new Exception("Psychologist schedule is full.");
            }
        }

        public void AddToSchedule(int date, Psychologist psychologist)
        {
            if (date == 0)
            {
                psychologist.Schedule.MondayConsultations++;
            }
            else if (date == 1)
            {
                psychologist.Schedule.ThursdayConsultations++;
            }
            else if (date == 2)
            {
                psychologist.Schedule.WednesdayConsultations++;
            }
            else if (date == 3)
            {
                psychologist.Schedule.ThursdayConsultations++;
            }
            else
            {
                psychologist.Schedule.FridayConsultations++;
            }

            psychologistRepository.Update(psychologist.Id, psychologist);
        }
    }
}
