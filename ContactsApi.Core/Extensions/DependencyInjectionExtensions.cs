using AutoMapper;
using ContactsApi.Core.Interfaces;
using ContactsApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactsApi.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddContactsApiCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IContactsService, ContactsService>();
            services.AddTransient<ISkillsService, SkillsService>();

            return services;
        }
    }
}
