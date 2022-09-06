using Core.Auth;
using System.ComponentModel.DataAnnotations;

namespace API.DTO.Objective
{
    public class ObjectiveDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        //public User? User { get; set; }

        //public int DepartmentId { get; set; }

        public string Owner { get; set; }

        public string DepartmentName { get; set; }

        public int TeamId { get; set; }

        public int SurObjectiveId { get; set; }

        public List<Core.Models.Objective> SubObjectives { get; set; } = new List<Core.Models.Objective>();

        public List<Core.Models.KeyResult> KeyResults { get; set; } = new List<Core.Models.KeyResult>();
    }
}