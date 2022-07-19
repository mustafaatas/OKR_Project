using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IKeyResultService
    {
        Task<IEnumerable<KeyResult>> GetAllKeyResults();
        Task<KeyResult> GetKeyResultById(int id);
        Task<KeyResult> CreateKeyResult(KeyResult newKeyResult);
        Task UpdateKeyResult(KeyResult keyResultToBeUpdated, KeyResult keyResult);
        Task DeleteKeyResult(KeyResult keyResult);
    }
}
