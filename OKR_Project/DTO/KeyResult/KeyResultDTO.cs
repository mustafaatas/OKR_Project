namespace API.DTO.KeyResult
{
    public class KeyResultDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        //public float StartValue { get; set; }

        //public float TargetValue { get; set; }

        public float Interval { get; set; }//{ get { return TargetValue - StartValue; } }

        public int SurObjectiveId { get; set; }
    }
}
