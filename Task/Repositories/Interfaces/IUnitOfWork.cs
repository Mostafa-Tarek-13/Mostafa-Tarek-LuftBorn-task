using System;
using System.Threading.Tasks;
using Task.Repositories.Interfaces;

namespace Task.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPlaylistRepository Playlists { get; }
        IUserRepository Users { get; }
        ISongRepository Songs { get; }
        IPlaylistSongRepository PlaylistSongs { get; }
        Task<int> SaveChangesAsync();
    }
}
