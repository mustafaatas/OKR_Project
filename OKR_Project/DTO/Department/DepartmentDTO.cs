using Core.Auth;
using Core.Models;

namespace API.DTO.Department
{
    public class DepartmentDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public List<Core.Models.Team>? TeamList { get; set; }

        public List<User>? UserList { get; set; }
    }
}