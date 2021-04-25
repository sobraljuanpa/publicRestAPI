using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Schedule
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MondayConsultations { get; set; }

        public int TuesdayConsultations { get; set; }

        public int WednesdayConsultations { get; set; }

        public int ThursdayConsultations { get; set; }

        public int FridayConsultations { get; set; }


    }
}
