using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Consultation
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public DateTime PatientBirthDate { get; set; }

        public string PatientEmail { get; set; }

        public string PatientPhone { get; set; }
    }
}
