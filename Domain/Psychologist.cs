using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Psychologist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PsychologistName { get; set; }

        public string PsychologistSurname { get; set; }

        public bool IsRemote { get; set; }

        public string Address { get; set; }

        public List<Problem> Expertise { get; set; }
    }
}
