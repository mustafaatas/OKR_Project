using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class KeyResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Deadline { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        public float StartValue { get; set; }

        public float TargetValue { get; set; }

        [NotMapped]
        public float Interval { get { return TargetValue - StartValue; } }

        public float ActualValue { get; set; }

        public Status Status { get; set; } = Status.Open;

        public int SurObjectiveId { get; set; }

        public Objective SurObjective { get; set; }
    }
}
