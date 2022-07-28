using Core.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        public List<User>? UserList { get; set; }
    }
}
