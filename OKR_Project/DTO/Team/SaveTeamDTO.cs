using Core.Auth;

namespace API.DTO.Team
{
    public class SaveTeamDTO
    {
        public string? Name { get; set; }

        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
