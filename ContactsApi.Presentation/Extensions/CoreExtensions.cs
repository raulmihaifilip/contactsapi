using ContactsApi.Core.Interfaces;
using ContactsApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApi.Presentation.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddContactsApiCore(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IContactsService, ContactsService>();
            services.AddTransient<ISkillsService, SkillsService>();

            return services;
        }
    }
}
