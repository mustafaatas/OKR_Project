using Core;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> CreateTeam(Team newTeam)
        {
            await _unitOfWork.Teams.AddAsync(newTeam);
            _unitOfWork.Commit();
            return newTeam;
        }

        public async Task DeleteTeam(Team team)
        {
            _unitOfWork.Teams.Remove(team);
            _unitOfWork.Commit();
        }

        public IQueryable<Team> GetAllTeams()
        {
            return _unitOfWork.Teams.GetAllAsync().Include(t => t.TeamUsers).ThenInclude(t => t.User);
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await GetAllTeams().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateTeam(Team teamToBeUpdated)
        {
            _unitOfWork.Teams.Update(teamToBeUpdated);
            _unitOfWork.Commit();
        }
    }
}
