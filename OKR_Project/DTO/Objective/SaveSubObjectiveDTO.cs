using System.ComponentModel.DataAnnotations;

namespace API.DTO.Objective
{
    public class SaveSubObjectiveDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Deadline { get; set; }

        public string MeasurementUnit { get; set; }

        public float StartValue { get; set; }

        public float TargetValue { get; set; }

        public float Interval { get { return TargetValue - StartValue; } }

        public int SurObjectiveId { get; set; }
    }
}
