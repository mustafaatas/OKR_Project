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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public IQueryable<User> GetUsersWithInclude()
        {
            return DataContext.Users.Include(i => i.Role).Include(i => i.Department).Include(i => i.TeamUsers).ThenInclude(i => i.Team).AsQueryable(); ;
        }

        public Task<User> GetWithUsersByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
