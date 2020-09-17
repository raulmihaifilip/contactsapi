using ContactsApi.Core.Interfaces;
using ContactsApi.Core.ViewModels;
using ContactsApi.Presentation.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactsApi.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerViewModel)
        {
            await _authService.RegisterAsync(registerViewModel);
            return Ok();
        }

         /// <summary>
         /// Loging and get the token
         /// </summary>
         /// <param name="loginViewModel"></param>
         /// <returns></returns>
        [HttpPut]        
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel loginViewModel)
        {
            return Ok(await _authService.LoginAsync(loginViewModel));
        }
    }
}
