using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.Repositories.Interfaces;

namespace Task.Repositories.Implementations
{
    public sealed class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _context;

        public PlaylistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Playlist> AddAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
            return playlist;
        }

        public async Task<IEnumerable<Playlist>> GetByUserIdAsync(int userId)
        {
            return await _context.Playlists
                .Where(playlist => playlist.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int playlistId)
        {
            return await _context.Playlists
                .AnyAsync(playlist => playlist.PlaylistId == playlistId);
        }

        public async Task<Playlist?> GetByIdAsync(int playlistId)
        {
            return await _context.Playlists
                .FirstOrDefaultAsync(playlist => playlist.PlaylistId == playlistId);
        }
    }
}
