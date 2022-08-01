using Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IRoleService
    {
        IQueryable<Role> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(Role newRole);
        Task UpdateRole(Role roleToBeUpdated, Role role);
        Task DeleteRole(Role role);
    }
}
