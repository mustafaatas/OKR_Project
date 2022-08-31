using Core;
using Core.Auth;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.AddAsync(newUser);
            _unitOfWork.Commit();
            return newUser;
        }

        public async Task DeleteUser(User user)
        {
            _unitOfWork.Users.Remove(user);
            _unitOfWork.Commit();
        }

        public IQueryable<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetUsersWithInclude();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task UpdateUser(User user, Role role)
        {
            //add userda yaptığın işlemleri yap, varsa önceki rolleri sil
            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.AddToRoleAsync(user, role.Name);

            
            _unitOfWork.Commit();
        }

        public string GetDepartmentOfUser(Guid userId)
        {
            var user = GetAllUsers().Where(p => p.Id == userId).FirstOrDefault();

            return user?.Department?.Name;
        }
    }
}