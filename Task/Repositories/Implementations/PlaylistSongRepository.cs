using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.Repositories.Interfaces;

namespace Task.Repositories.Implementations
{
    public sealed class PlaylistSongRepository : IPlaylistSongRepository
    {
        private readonly AppDbContext _context;

        public PlaylistSongRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PlaylistSong> AddAsync(PlaylistSong playlistSong)
        {
            await _context.PlaylistSongs.AddAsync(playlistSong);
            return playlistSong;
        }

        public async Task<PlaylistSong?> GetAsync(int playlistId, int songId)
        {
            return await _context.PlaylistSongs
                .FirstOrDefaultAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);
        }

        public async Task<bool> ExistsAsync(int playlistId, int songId)
        {
            return await _context.PlaylistSongs
                .AnyAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);
        }

        public void Remove(PlaylistSong playlistSong)
        {
            _context.PlaylistSongs.Remove(playlistSong);
        }
    }
}
