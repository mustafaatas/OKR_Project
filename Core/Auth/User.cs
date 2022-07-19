using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class User: IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
