using Core.Auth;

namespace API.DTO.Team
{
    public class SaveTeamDTO
    {
        public string? Name { get; set; }

        public int? DepartmentId { get; set; }

        public List<Guid>? UserIdList { get; set; }
    }
}
