using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITeamRepository: IRepository<Team>
    {
        Task<IEnumerable<Team>> GetAllWithTeamsAsync();
        Task<Team> GetWithTeamsByIdAsync(int id);
    }
}
