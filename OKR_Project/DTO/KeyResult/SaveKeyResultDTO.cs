using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTO.KeyResult
{
    public class SaveKeyResultDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        //public DateTime? Deadline { get; set; }

        public string MeasurementUnit { get; set; }

        public float StartValue { get; set; }

        public float TargetValue { get; set; }

        //public float? Interval { get { return TargetValue - StartValue; } }

        public float ActualValue { get; set; }

        public string Status { get; set; }

        public int SurObjectiveId { get; set; }
    }
}
