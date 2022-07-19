using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IKeyResultRepository:IRepository<KeyResult>
    {
        Task<IEnumerable<KeyResult>> GetAllWithKeyResultsAsync();
        Task<KeyResult> GetWithKeyResultsByIdAsync(int id);
    }
}
