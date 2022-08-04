using Core.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Team>? TeamList{ get; set; } = new List<Team>();

        public List<User>? UserList{ get; set; } = new List<User>();
    }
}
