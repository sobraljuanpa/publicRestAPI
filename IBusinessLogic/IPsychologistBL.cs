using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.DTOs;

namespace IBusinessLogic
{
    public interface IPsychologistBL
    {
        
        //para problema y mas
        public List<PsychologistDTO> GetPsychologists();
        
        //para problema especifico
        public List<Psychologist> GetSpecialists(Problem problem);
        
        public void UpdateSchedule(int psychologistId, Schedule schedule);

        public Psychologist AddPsychologist(PsychologistDTO psychologist);

        public Schedule AddSchedule(Schedule schedule);

        public Psychologist AddProblemToPsychologist(Psychologist psychologist, int problem);

        public Psychologist AddScheduleToPsychologist(Psychologist psychologist, int id);

        public PsychologistDTO GetPsychologist(int id);

        public Schedule GetSchedule(int id);

        public void DeletePsychologist(int id);
       
        public void UpdatePsychologist(int id, PsychologistDTO psychologist);

    }
}
