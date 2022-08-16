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
            _unitOfWork.Commit();
            return newObjective;
        }

        public async Task DeleteObjective(Objective obj)
        {
            var objective = _unitOfWork.Objectives.GetAllAsync().FirstOrDefault(d => d.Id == obj.Id);
            if (objective == null)
            {
                return;
            }

            var objectiveToDelete = await DeleteSubObjectives(objective.Id);
            objectiveToDelete.Add(objective);

            _unitOfWork.Objectives.RemoveRange(objectiveToDelete);
            _unitOfWork.Commit();
        }

        public async Task<List<Objective>> DeleteSubObjectives(int objectiveId)
        {
            var subObjectives = await _unitOfWork.Objectives.GetAllAsync().Where(d => d.SurObjectiveId == objectiveId).ToListAsync();

            var allObjectives = new List<Objective>();
            foreach (var subObjective in subObjectives)
            {
                allObjectives.Add(subObjective);
                allObjectives.AddRange(await DeleteSubObjectives(subObjective.Id));
            }

            return allObjectives;
        }

        public IQueryable<Objective> GetAllObjectives()
        {
            return _unitOfWork.Objectives.GetAllAsync().Include(i => i.User).Include(i => i.KeyResults).Include(i => i.User);
        }

        public async Task<Objective> GetObjectiveById(int id)
        {
            return await _unitOfWork.Objectives.GetByIdAsync(id);
        }

        public async Task UpdateObjective(Objective objectiveToBeUpdated, Objective objective)
        {
            objectiveToBeUpdated.Title = objective.Title;
            objectiveToBeUpdated.Description = objective.Description;
            objectiveToBeUpdated.UserId = objective.UserId;
            objectiveToBeUpdated.TeamId = objective.TeamId;

            _unitOfWork.Commit();
        }
    }
}