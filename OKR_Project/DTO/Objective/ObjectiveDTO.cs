using Core.Auth;

namespace API.DTO.Objective
{
    public class ObjectiveDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? Deadline { get; set; }

        public User? Owner { get; set; }

        //public Core.Models.Department? Department { get; set; }

        //public Core.Models.Team? Team { get; set; }

        //public Core.Models.Objective? SurObjective { get; set; }

        public int DepartmentId { get; set; }

        public int TeamId { get; set; }

        public int SurObjectiveId { get; set; }

        public List<Core.Models.Objective>? SubObjectiveList { get; set; }

        public List<Core.Models.KeyResult>? KeyResultList { get; set; }
    }
}