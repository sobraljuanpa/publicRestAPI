using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.DTOs;

namespace IBusinessLogic
{
    public interface IConsultationBL
    {
        public List<Consultation> GetConsultationsByPsychologist(int id);

        public List<ConsultationDTO> GetConsultations();

        public ConsultationDTO Get(int id);

        public Consultation CreateConsultation(ConsultationDTO consultation);
    }
}
