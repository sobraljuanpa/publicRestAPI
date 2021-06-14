using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class ConsultationBL : IConsultationBL
    {
        private readonly IRepository<Consultation> consultationRepository;

        private readonly IRepository<Psychologist> psychologistRepository;

        private readonly IRepository<Problem> problemRepository;

        private readonly ConsultationValidator consultationValidator;

        public ConsultationBL(IRepository<Consultation> consultationRepository,
                              IRepository<Psychologist> psychologistRepository,
                              IRepository<Problem> problemRepository)
        {
            this.consultationRepository = consultationRepository;
            this.psychologistRepository = psychologistRepository;
            this.problemRepository = problemRepository;
            this.consultationValidator = new ConsultationValidator(this.consultationRepository, 
                                                                   this.psychologistRepository,
                                                                   this.problemRepository);
        }

        public List<Consultation> GetConsultations()
        {
            return consultationRepository.GetAll().ToList();
        }

        public Consultation Get(int id)
        {
            consultationValidator.IdValidRange(id);
            Consultation consultation = consultationRepository.Get(id);

            return consultation;
        }

        public List<Consultation> GetConsultationsByPsychologist(int id)
        {
            consultationValidator.IdValidRangePs(id);
            List<Consultation> consultations = consultationValidator.FindConsultations(id);

            return consultations;
        }

        public void ValidDuration(Consultation consultation)
        {
            if (consultation.Duration != 1 &&
                consultation.Duration != 1.5 &&
                consultation.Duration != 2)
            {
                throw new Exception("Invalid duration");
            }
        }

        public void ValidBonus(Consultation consultation)
        {
            if (consultation.Bonus != 15 &&
                consultation.Bonus != 25 &&
                consultation.Bonus != 50)
            {
                throw new Exception("Invalid bonus");
            }
        }

        public Consultation CreateConsultation(Consultation consultation)
        {
            ValidBonus(consultation);
            ValidDuration(consultation);
            consultationValidator.AssignPsychologist(consultation);
            consultationValidator.IdValidRangePs(consultation.Psychologist.Id);
            consultationValidator.ValidSchedule(consultation.Psychologist);
            consultationValidator.ValidAddress(consultation);
            consultationValidator.FullSchedule(consultation);
            consultationValidator.AddToSchedule(consultation.Date, consultation.Psychologist);
            consultationRepository.Add(consultation);

            return consultation;
        }
    }
}
