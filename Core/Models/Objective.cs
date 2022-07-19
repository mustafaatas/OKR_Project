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

        public Guid? OwnerId { get; set; }

        public User? Owner { get; set; }

        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }

        public int? TeamId { get; set; }

        public Team? Team { get; set; }

        public int? SurObjectiveId { get; set; }

        public Objective? SurObjective { get; set; }

        public ICollection<Objective>? SubObjectiveList { get; set; }

        public ICollection<KeyResult>? KeyResultList { get; set; }
    }
}