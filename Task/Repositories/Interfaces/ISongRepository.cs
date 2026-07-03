using System.Threading.Tasks;

namespace Task.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<bool> ExistsAsync(int songId);
    }
}
