using Core.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Objective> Objectives { get; set; } = new List<Objective>();

        public Guid? LeaderId { get; set; }

        [ForeignKey("LeaderId")]

        public User? Leader { get; set; }
    }
}
