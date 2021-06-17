namespace Domain.DTOs
{
    public class PsychologistDTO
    {
        public int Id { get; set; }

        public string PsychologistName { get; set; }

        public string PsychologistSurname { get; set; }

        public bool IsRemote { get; set; }

        public string Address { get; set; }

        public int ActiveYears { get; set; }

        public int Fee { get; set; }

        public int ScheduleId { get; set; }

        public int ExpertiseId1 { get; set; }

        public int ExpertiseId2 { get; set; }

        public int ExpertiseId3 { get; set; }

    }
}
