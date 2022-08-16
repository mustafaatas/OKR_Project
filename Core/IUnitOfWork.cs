using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork: IDisposable
    {
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get; }
        IDepartmentRepository Departments { get; }
        IKeyResultRepository KeyResults { get; }
        IObjectiveRepository Objectives { get; }
        ITeamRepository Teams { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        int Commit();
    }
}
