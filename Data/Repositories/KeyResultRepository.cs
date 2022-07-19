using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class KeyResultRepository : Repository<KeyResult>, IKeyResultRepository
    {
        public KeyResultRepository(DataContext context) : base(context)
        { }

        public Task<IEnumerable<KeyResult>> GetAllWithKeyResultsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<KeyResult> GetWithKeyResultsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
