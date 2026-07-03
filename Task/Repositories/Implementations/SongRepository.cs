using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Repositories.Interfaces;

namespace Task.Repositories.Implementations
{
    public sealed class SongRepository : ISongRepository
    {
        private readonly AppDbContext _context;

        public SongRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int songId)
        {
            return await _context.Songs.AnyAsync(song => song.SongId == songId);
        }
    }
}
