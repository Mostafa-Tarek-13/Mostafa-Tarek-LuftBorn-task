using System.Threading.Tasks;
using Task.Data;
using Task.Repositories.Interfaces;

namespace Task.Repositories.Implementations
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Playlists = new PlaylistRepository(context);
            Users = new UserRepository(context);
            Songs = new SongRepository(context);
            PlaylistSongs = new PlaylistSongRepository(context);
        }

        public IPlaylistRepository Playlists { get; }
        public IUserRepository Users { get; }
        public ISongRepository Songs { get; }
        public IPlaylistSongRepository PlaylistSongs { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
