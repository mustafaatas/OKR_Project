using Core.Auth;

namespace API.DTO.Team
{
    public class TeamDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
