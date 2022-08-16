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

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return _unitOfWork.Users.GetAllAsync().Include(i => i.Role).Include(i => i.Department).Include(i => i.TeamUsers).ThenInclude(i => i.Team);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task UpdateUser(User userToBeUpdated, User user)
        {
            userToBeUpdated.RoleId = user.RoleId;
            _unitOfWork.Commit();
        }

        public string GetDepartmentOfUser(Guid userId)
        {
            var user = GetAllUsers().Where(p => p.Id == userId).FirstOrDefault();

            return user.Department.Name;
        }
    }
}