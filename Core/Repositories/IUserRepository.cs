using Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        IQueryable<User> GetUsersWithInclude();
        Task<User> GetWithUsersByIdAsync(int id);
    }
}
