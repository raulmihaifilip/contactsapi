using ContactsApi.Core.Entities;
using ContactsApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContactsApi.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ContactsApiDbContext _dbContext;

        public AuthRepository(ContactsApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower()));
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
