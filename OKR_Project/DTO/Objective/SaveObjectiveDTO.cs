using Core.Auth;

namespace API.DTO.Objective
{
    public class SaveObjectiveDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid OwnerId { get; set; }

        public int TeamId { get; set; }

        //public int DepartmentId { get; set; }

        //public Core.Models.Objective? SurObjective { get; set; }

        //public List<Core.Models.Objective>? SubObjectiveList { get; set; }

        //public List<Core.Models.KeyResult>? KeyResultList { get; set; }
    }
}