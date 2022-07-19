using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(DataContext context) : base(context)
        { }

        public Task<IEnumerable<Objective>> GetAllWithObjectivesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Objective> GetWithObjectivesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
