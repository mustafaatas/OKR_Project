using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(DataContext context): base(context)
        { }

        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            return await DataContext.Artists
                .Include(a => a.Musics)
                .ToListAsync();
        }

        public async Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return await DataContext.Artists
                .Include(a => a.Musics)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
