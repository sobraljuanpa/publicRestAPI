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

        [ForeignKey("ProblemId")]
        public Problem Problem { get; set; }

        public int ProblemId { get; set; }

        public Psychologist Psychologist { get; set; }

        public bool IsRemote { get; set; }

        public string Address { get; set; }

        public int Date { get; set; }

        public int Duration { get; set; }

        public int Bonus { get; set; }

        public int Cost { get; set; }

    }
}
