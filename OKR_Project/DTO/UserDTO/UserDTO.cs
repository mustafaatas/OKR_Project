namespace API.DTO.UserDTO
{
    public class UserDTO
    {
        public virtual string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? TeamId { get; set; }

        public int? DepartmentId { get; set; }

        public Guid? RoleId { get; set; }
    }
}
