using Core;
using Core.Auth;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService: IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> CreateRole(Role newRole)
        {
            await _unitOfWork.Roles.AddAsync(newRole);
            await _unitOfWork.CommitAsync();
            return newRole;
        }

        public async Task DeleteRole(Role role)
        {
            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<Role> GetAllRoles()
        {
            return _unitOfWork.Roles.GetAllAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _unitOfWork.Roles.GetByIdAsync(id);
        }

        public async Task UpdateRole(Role roleToBeUpdated, Role role)
        {
            roleToBeUpdated.Name = role.Name;
            await _unitOfWork.CommitAsync();
        }
    }
}
