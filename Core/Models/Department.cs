using Core.Auth;

namespace Core.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users{ get; set; } = new List<User>();

        public List<Objective> Objectives{ get; set; } = new List<Objective>();
    }
}
