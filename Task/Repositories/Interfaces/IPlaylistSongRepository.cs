using System.Threading.Tasks;
using Task.Models;

namespace Task.Repositories.Interfaces
{
    public interface IPlaylistSongRepository
    {
        Task<PlaylistSong> AddAsync(PlaylistSong playlistSong);
        Task<PlaylistSong?> GetAsync(int playlistId, int songId);
        Task<bool> ExistsAsync(int playlistId, int songId);
        void Remove(PlaylistSong playlistSong);
    }
}
