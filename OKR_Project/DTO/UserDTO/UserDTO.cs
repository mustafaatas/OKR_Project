using Core.Auth;

namespace API.DTO.UserDTO
{
    public class UserDTO
    {
        public virtual string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string>? TeamNames { get; set; }

        public string? DepartmentName { get; set; }

        public string? RoleName { get; set; }
    }
}
