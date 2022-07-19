using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllWithDepartmentsAsync();
        Task<Department> GetWithDepartmentsByIdAsync(int id);
    }
}
