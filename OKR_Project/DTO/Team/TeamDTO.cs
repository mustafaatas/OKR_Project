using Core.Auth;

namespace API.DTO.Team
{
    public class TeamDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? DepartmentId { get; set; }

        public ICollection<User>? UserList { get; set; }
    }
}
