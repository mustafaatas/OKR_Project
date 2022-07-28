namespace API.DTO.UserDTO
{
    public class UpdateUserDTO
    {
        public Guid? RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? TeamId { get; set; }
    }
}
