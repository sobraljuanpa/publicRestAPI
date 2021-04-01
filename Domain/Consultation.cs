using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Consultation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PatientName { get; set; }

        public DateTime PatientBirthDate { get; set; }

        public string PatientEmail { get; set; }

        public string PatientPhone { get; set; }

        public Problem Problem { get; set; }

        public Psychologist Psychologist { get; set; }
    }
}
