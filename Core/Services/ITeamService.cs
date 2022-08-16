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
        IQueryable<Team> GetAllTeams();
        Task<Team> GetTeamById(int id);
        Task<Team> CreateTeam(Team newTeam);
        void UpdateTeam(Team teamToBeUpdated);
        Task DeleteTeam(Team team);
    }
}
