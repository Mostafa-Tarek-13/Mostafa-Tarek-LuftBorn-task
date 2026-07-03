using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Contracts.Requests;
using Task.Contracts.Responses;

namespace Task.Services.Interfaces
{
    public interface IPlaylistService
    {
        System.Threading.Tasks.Task<PlaylistResponse> CreatePlaylistAsync(PlaylistCreateRequest request);
        System.Threading.Tasks.Task<IEnumerable<PlaylistResponse>> GetPlaylistsByUserAsync(int userId);
        System.Threading.Tasks.Task<PlaylistSongResponse> AddSongToPlaylistAsync(int playlistId, AddSongToPlaylistRequest request);
        System.Threading.Tasks.Task<PlaylistSongResponse> UpdatePlaylistSongAsync(int playlistId, int songId, UpdatePlaylistSongRequest request);
        System.Threading.Tasks.Task DeleteSongFromPlaylistAsync(int playlistId, int songId);
    }
}
