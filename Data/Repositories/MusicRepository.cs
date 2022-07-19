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
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        public MusicRepository(DataContext context)
             : base(context)
        { }

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await DataContext.Musics
                .Include(m => m.Artist)
                .ToListAsync();
        }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await DataContext.Musics
                .Include(m => m.Artist)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await DataContext.Musics
                .Include(m => m.Artist)
                .Where(m => m.ArtistId == artistId)
                .ToListAsync();
        }

        private DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
