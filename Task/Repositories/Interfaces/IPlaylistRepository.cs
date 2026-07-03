using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Repositories.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<Playlist> AddAsync(Playlist playlist);
        Task<IEnumerable<Playlist>> GetByUserIdAsync(int userId);
        Task<bool> ExistsAsync(int playlistId);
        Task<Playlist?> GetByIdAsync(int playlistId);
    }
}
