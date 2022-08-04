using Core.Auth;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Objective
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Deadline { get; set; }

        public Guid? OwnerId { get; set; }

        public User? Owner { get; set; }

        public int? TeamId { get; set; }

        public Team? Team { get; set; }

        public int? SurObjectiveId { get; set; }

        public Objective? SurObjective { get; set; }

        public List<Objective>? SubObjectiveList { get; set; } = new List<Objective>();

        public List<KeyResult>? KeyResultList { get; set; } = new List<KeyResult>();
    }
}