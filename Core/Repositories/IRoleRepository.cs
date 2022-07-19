using Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        Task<IEnumerable<Role>> GetAllWithRolesAsync();
        Task<Role> GetWithRolesByIdAsync(int id);
    }
}
