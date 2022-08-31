using Core.Auth;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User newUser);
        Task UpdateUser(User user, Role role);
        Task DeleteUser(User user);
        string GetDepartmentOfUser(Guid userId);
    }
}
