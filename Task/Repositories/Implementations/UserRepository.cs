using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Repositories.Interfaces;

namespace Task.Repositories.Implementations
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(user => user.UserId == userId);
        }
    }
}
