using System.ComponentModel.DataAnnotations;

namespace API.DTO.Objective
{
    public class SaveSubObjectiveDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Guid UserId { get; set; }

        public int SurObjectiveId { get; set; }
    }
}
