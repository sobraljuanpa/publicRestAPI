﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IPsychologistBL
    {
        
        //para problema y mas
        public List<Psychologist> GetPsychologists();
        
        //para problema especifico
        public List<Psychologist> GetSpecialists(Problem problem);
        
        public void UpdateSchedule(int psychologistId, Schedule schedule);

        public Psychologist AddPsychologist(Psychologist psychologist);

        public Psychologist GetPsychologist(int id);

        public void DeletePsychologist(int id);
       
        public void UpdatePsychologist(int id, Psychologist psychologist);

    }
}
