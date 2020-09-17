using ContactsApi.Core.Interfaces;
using ContactsApi.Core.Models;
using ContactsApi.Presentation.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ContactsApi.Presentation.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddContactsApiAuthentication(this IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            ValidIssuer = authOptions.Issuer,
                            ValidAudience = authOptions.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecureKey))
                        };
                    });

            services.AddTransient<IAuthClaimsService, AuthClaimsService>();

            return services;
        }
    }
}
