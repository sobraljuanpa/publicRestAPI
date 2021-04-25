using Domain;
using IBusinessLogic;
using IDataAccess;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class ConsultationBL : IConsultationBL
    {
        private readonly IRepository<Consultation> consultationRepository;
        private readonly IRepository<Psychologist> psychologistRepository;

        public ConsultationBL(IRepository<Consultation> consultationRepository,
                              IRepository<Psychologist> psychologistRepository)
        {
            this.consultationRepository = consultationRepository;
            this.psychologistRepository = psychologistRepository;
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

        public void ValidSchedule(Psychologist psychologist)
        {
            if (psychologist.Schedule == null)
            {
                throw new Exception("Invalid schedule.");
            }
        }

        public void ValidRemoteAddress(String address)
        {
            string format = @"\A[https]+(\://)[betterCalm]+(\.)[com]+(\.)[uy]+(\/)[meeting_id]+(\/)[codigo]";
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

        public Consultation CreateConsultation(Consultation consultation)
        {
            IdValidRangePs(consultation.Psychologist.Id);
            ValidSchedule(consultation.Psychologist);
            ValidAddress(consultation);
            FullSchedule(consultation);
            AddToSchedule(consultation.Date, consultation.Psychologist);
            consultationRepository.Add(consultation);

            return consultation;
        }

        public void IdValidRange(int id)
        {
            if (id <= 0 || id > consultationRepository.GetAll().ToList().Count())
            {
                throw new Exception("No consultation associated to given id");
            }

        }

        public Consultation Get(int id)
        {
            IdValidRange(id);
            Consultation consultation = consultationRepository.Get(id);

            return consultation;
        }

        public List<Consultation> GetConsultations ()
        {
            return consultationRepository.GetAll().ToList();
        }

        public void IdValidRangePs(int id)
        {
            if (id <= 0 || id > psychologistRepository.GetAll().ToList().Count())
            {
                throw new Exception("No psychologist associated to given id");
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

        public List<Consultation> GetConsultationsByPsychologist(int id)
        {
            IdValidRangePs(id);
            List<Consultation> consultations = FindConsultations(id);

            return consultations;
        }
    }
}
