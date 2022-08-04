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
            await _unitOfWork.CommitAsync();
            return newObjective;
        }

        public async Task DeleteObjective(Objective obj)
        {
            var objective = _unitOfWork.Objectives.GetAllAsync().FirstOrDefault(d => d.Id == obj.Id);
            if (objective == null)
            {
                return;
            }
            CheckSubObjectives(obj);

            var objectiveToDelete = await DeleteSubObjectives(objective.Id);
            objectiveToDelete.Add(objective);

            _unitOfWork.Objectives.RemoveRange(objectiveToDelete);
            await _unitOfWork.CommitAsync();
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
            return _unitOfWork.Objectives.GetAllAsync();
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
            objectiveToBeUpdated.TeamId = objective.TeamId;

            await _unitOfWork.CommitAsync();
        }

        public bool CheckSubObjectives(Objective objective)
        {
            var surObjective = _unitOfWork.Objectives.GetAllAsync().Where(e => e.Id == objective.SurObjectiveId).ToList(); // get parent from child
            //var subObjectives = _unitOfWork.Objectives.GetAllAsync().Where(e => objective.SubObjectiveList.Any()).ToList();

            //var keyResults = _unitOfWork.KeyResults.GetAllAsync().Where(e => e.).ToList();

            var keyResult = surObjective.Where(e => e.KeyResultList == objective.KeyResultList).ToList();

            if(keyResult == null)
            {
                return false;
            }

            return true;
        }
    }
}