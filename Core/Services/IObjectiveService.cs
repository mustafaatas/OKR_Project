using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IObjectiveService
    {
        Task<IEnumerable<Objective>> GetAllObjectives();
        Task<Objective> GetObjectiveById(int id);
        Task<Objective> CreateObjective(Objective newObjective);
        Task UpdateObjective(Objective objectiveToBeUpdated, Objective objective);
        Task DeleteObjective(Objective objective);
    }
}
