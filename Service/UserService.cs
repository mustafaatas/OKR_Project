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
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        public async Task DeleteUser(User user)
        {
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAllAsync().Include(i => i.Team).ThenInclude(i => i.Department);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task UpdateUser(User userToBeUpdated, User user)
        {
            var x = GetAllUsers().Where(p => p.Id == userToBeUpdated.Id).FirstOrDefault();
            x.Role.Name = user.Role.Name;
            await _unitOfWork.CommitAsync();
        }

        public string GetDepartmentOfUser(Guid userId)
        {
            var user = GetAllUsers().Where(p => p.Id == userId).FirstOrDefault();

            return user?.Team?.Department.Name;
        }
    }
}