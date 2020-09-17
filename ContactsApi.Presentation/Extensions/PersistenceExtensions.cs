using ContactsApi.Core.Interfaces;
using ContactsApi.Persistence;
using ContactsApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApi.Presentation.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddContactsApiPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ContactsApiDbContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IContactsRepository, ContactsRepository>();
            services.AddTransient<ISkillsRepository, SkillsRepository>();

            return services;
        }
    }
}
