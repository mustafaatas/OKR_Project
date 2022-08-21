using Core.Auth;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Objective
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid? UserId { get; set; }

        public User? User { get; set; }

        public int? TeamId { get; set; }

        public Team? Team { get; set; }

        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }

        public int? SurObjectiveId { get; set; }

        public Objective? SurObjective { get; set; }

        public List<Objective> SubObjectives { get; set; } = new List<Objective>();

        public List<KeyResult> KeyResults { get; set; } = new List<KeyResult>();
    }
}