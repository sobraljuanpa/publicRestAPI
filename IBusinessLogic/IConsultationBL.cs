using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IConsultationBL
    {
        public List<Consultation> GetConsultationsByPsychologist(int id);

        public List<Consultation> GetConsultations();

        public Consultation Get(int id);

        public Consultation CreateConsultation(Consultation consultation);
    }
}
