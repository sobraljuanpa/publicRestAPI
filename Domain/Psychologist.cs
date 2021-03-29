using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Psychologist
    {
        public int Id { get; set; }

        public string PsychologistName { get; set; }

        public string PsychologistSurname { get; set; }

        public bool IsRemote { get; set; }

        public string Address { get; set; }

        public List<Problem> Expertise { get; set; }
    }
}
