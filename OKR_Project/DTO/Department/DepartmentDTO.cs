using Core.Auth;
using Core.Models;

namespace API.DTO.Department
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public Guid? LeaderId { get; set; }
    }
}