using Core;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObjectiveService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Objective> CreateObjective(Objective newObjective)
        {
            await _unitOfWork.Objectives.AddAsync(newObjective);
            await _unitOfWork.CommitAsync();
            return newObjective;
        }

        public async Task DeleteObjective(Objective objective)
        {
            _unitOfWork.Objectives.Remove(objective);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Objective>> GetAllObjectives()
        {
            return await _unitOfWork.Objectives.GetAllAsync();
        }

        public async Task<Objective> GetObjectiveById(int id)
        {
            return await _unitOfWork.Objectives.GetByIdAsync(id);
        }

        public async Task UpdateObjective(Objective objectiveToBeUpdated, Objective objective)
        {
            objectiveToBeUpdated.Title = objective.Title;
            objectiveToBeUpdated.Description = objective.Description;
            objectiveToBeUpdated.OwnerId = objective.OwnerId;
            objectiveToBeUpdated.DepartmentId = objective.DepartmentId;
            objectiveToBeUpdated.TeamId = objective.TeamId;

            await _unitOfWork.CommitAsync();
        }
    }
}
