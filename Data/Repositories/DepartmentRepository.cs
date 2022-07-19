using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    internal class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext context) : base(context)
        { }

        public Task<IEnumerable<Department>> GetAllWithDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetWithDepartmentsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
