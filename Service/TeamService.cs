using Core;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _unitOfWork.CommitAsync();
            return newTeam;
        }

        public async Task DeleteTeam(Team team)
        {
            _unitOfWork.Teams.Remove(team);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<Team> GetAllTeams()
        {
            return _unitOfWork.Teams.GetAllAsync().Include(t => t.UserList);
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _unitOfWork.Teams.GetByIdAsync(id);
        }

        public async Task UpdateTeam(Team teamToBeUpdated, Team team)
        {
            teamToBeUpdated.DepartmentId = team.DepartmentId;
            await _unitOfWork.CommitAsync();
        }
    }
}
