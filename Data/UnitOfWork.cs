using Core;
using Core.Repositories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private MusicRepository _musicRepository;
        private ArtistRepository _artistRepository;
        private DepartmentRepository _departmentRepository;
        private TeamRepository _teamRepository;
        private KeyResultRepository _keyResultRepository;
        private ObjectiveRepository _objectiveRepository;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public IDepartmentRepository Departments => _departmentRepository = _departmentRepository ?? new DepartmentRepository(_context);

        public IKeyResultRepository KeyResults => _keyResultRepository = _keyResultRepository ?? new KeyResultRepository(_context);

        public IObjectiveRepository Objectives => _objectiveRepository = _objectiveRepository ?? new ObjectiveRepository(_context);

        public ITeamRepository Teams => _teamRepository = _teamRepository = _teamRepository ?? new TeamRepository(_context);

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

        public IRoleRepository Roles => _roleRepository = _roleRepository ?? new RoleRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
