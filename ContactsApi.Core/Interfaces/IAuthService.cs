using ContactsApi.Core.ViewModels;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginViewModel loginViewModel);
        Task RegisterAsync(RegisterViewModel registerViewModel);
    }
}