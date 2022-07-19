using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int id);
        Task<Team> CreateTeam(Team newTeam);
        Task UpdateTeam(Team teamToBeUpdated, Team team);
        Task DeleteTeam(Team team);
    }
}
