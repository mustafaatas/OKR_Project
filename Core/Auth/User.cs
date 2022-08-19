using Core.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Auth
{
    public class User: IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public List<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();

        public List<Objective> Objectives { get; set; } = new List<Objective>();
    }
}
