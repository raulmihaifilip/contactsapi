using ContactsApi.Core.Entities;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<User> GetUserAsync(string username);
        Task AddUserAsync(User user);
    }
}
