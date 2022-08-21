using Core.Auth;

namespace API.DTO.Objective
{
    public class SaveObjectiveDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Guid UserId { get; set; }

        public int? TeamId { get; set; }
    }
}