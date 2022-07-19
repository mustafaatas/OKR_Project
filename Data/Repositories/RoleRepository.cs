using Core.Auth;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context) { }

        public Task<IEnumerable<Role>> GetAllWithRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetWithRolesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
