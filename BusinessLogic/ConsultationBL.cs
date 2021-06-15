using Domain;
using Domain.DTOs;
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

        public List<ConsultationDTO> GetConsultations()
        {
            var consultations = consultationRepository.GetAll().ToList();
            var auxConsultation = new List<ConsultationDTO>();

            foreach (Consultation consultation in consultations)
            {
                auxConsultation.Add(ToDTO(consultation));
            }

            return auxConsultation;


        }

        public ConsultationDTO ToDTO(Consultation consultation)
        {
            var auxConsultation = new ConsultationDTO
            {
                Id = consultation.Id,
                PatientName = consultation.PatientName,
                PatientBirthDate = consultation.PatientBirthDate,
                PatientEmail = consultation.PatientEmail,
                PatientPhone = consultation.PatientPhone,
                IsRemote = consultation.IsRemote,
                ProblemId = consultation.ProblemId,
                Address = consultation.Address,
                Date = consultation.Date,
                Duration = consultation.Duration,
                Bonus = consultation.Bonus,
                Cost = consultation.Cost
            };

            return auxConsultation;
        }

        public ConsultationDTO Get(int id)
        {
            consultationValidator.IdValidRange(id);
            Consultation consultation = consultationRepository.Get(id);

            return ToDTO(consultation);
        }

        public List<Consultation> GetConsultationsByPsychologist(int id)
        {
            consultationValidator.IdValidRangePs(id);
            List<Consultation> consultations = consultationValidator.FindConsultations(id);

            return consultations;
        }

        public Consultation ToEntity(ConsultationDTO consultation)
        {
            var problem = problemRepository.Get(consultation.ProblemId);

            Consultation aux = new Consultation
            {
                PatientName = consultation.PatientName,
                PatientBirthDate = consultation.PatientBirthDate,
                PatientEmail = consultation.PatientEmail,
                PatientPhone = consultation.PatientPhone,
                IsRemote = consultation.IsRemote,
                Problem = problem,
                ProblemId = consultation.ProblemId,
                Address = consultation.Address,
                Date = consultation.Date,
                Duration = consultation.Duration,
                Bonus = consultation.Bonus,
                Cost = consultation.Cost
            };

            return aux;
        }

        public Consultation CreateConsultation(ConsultationDTO consultation)
        {
            Consultation auxConsultation = ToEntity(consultation);

            consultationValidator.ValidBonus(auxConsultation);
            consultationValidator.ValidDuration(auxConsultation);
            consultationValidator.AssignPsychologist(auxConsultation);
            consultationValidator.CalculateConsultationCost(auxConsultation);
            consultationValidator.IdValidRangePs(auxConsultation.Psychologist.Id);
            consultationValidator.ValidSchedule(auxConsultation.Psychologist);
            consultationValidator.ValidAddress(auxConsultation);
            consultationValidator.FullSchedule(auxConsultation);
            consultationValidator.AddToSchedule(consultation.Date, auxConsultation.Psychologist);
            consultationRepository.Add(auxConsultation);

            return consultationRepository.Get(auxConsultation.Id);
        }
    }
}
