using ContactsApi.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace ContactsApi.Presentation.Authentication
{
    public class AuthClaimsService : IAuthClaimsService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
