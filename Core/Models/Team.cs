namespace Core.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Objective> Objectives { get; set; } = new List<Objective>();

        public List<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();


        public void SetTeamUsers(List<Guid> userIds)
        {
            var existingUsers = TeamUsers.Select(p => p.UserId).ToList();

            var userIdsToBeDeleted = existingUsers.Except(userIds).ToList();
            var userIdsToBeAdded = userIds.Except(existingUsers).ToList();

            foreach(var userIdToBeDeleted in userIdsToBeDeleted)
            {
                DeleteUserFromTeam(userIdToBeDeleted);
            }

            foreach (var userIdToBeAdded in userIdsToBeAdded)
            {
                AddUserToTeam(userIdToBeAdded);
            }
        }

        public void AddUserToTeam(Guid userId)
        {
            var teamUser = new TeamUser { 
                UserId = userId
            };

            TeamUsers.Add(teamUser);
        }

        public void DeleteUserFromTeam(Guid userId)
        {
            var userToBeDeleted = TeamUsers.FirstOrDefault(p => p.UserId == userId);
            TeamUsers.Remove(userToBeDeleted);
        }
    }  
}
