using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IObjectiveRepository:IRepository<Objective>
    {
        Task<IEnumerable<Objective>> GetAllWithObjectivesAsync();
        Task<Objective> GetWithObjectivesByIdAsync(int id);
    }
}
