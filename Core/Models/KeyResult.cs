using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class KeyResult
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? Deadline { get; set; }

        public string MeasurementUnit { get; set; }

        public float StartValue { get; set; }

        public float TargetValue { get; set; }

        public float Interval { get { return TargetValue - StartValue; } }

        public float ActualValue { get; set; }

        public string Status { get; set; }

        public int SurObjectiveId { get; set; }

        public Objective SurObjective { get; set; }
    }
}
