using System.Threading.Tasks;
using Task.Models;

namespace Task.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(int userId);
    }
}
