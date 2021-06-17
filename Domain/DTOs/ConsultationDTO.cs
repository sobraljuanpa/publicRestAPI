using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class ConsultationDTO
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public DateTime PatientBirthDate { get; set; }

        public string PatientEmail { get; set; }

        public string PatientPhone { get; set; }

        public int ProblemId { get; set; }

        public bool IsRemote { get; set; }

        public string Address { get; set; }

        public int Date { get; set; }

        public int Duration { get; set; }

        public int Bonus { get; set; }

        public int Cost { get; set; }
    }
}
