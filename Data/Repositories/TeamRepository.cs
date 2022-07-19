using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TeamRepository: Repository<Team>, ITeamRepository
    {
        public TeamRepository(DataContext context) : base(context)
        { }

        public Task<IEnumerable<Team>> GetAllWithTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetWithTeamsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
