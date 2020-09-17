using ContactsApi.Presentation.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApi.Presentation.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentationExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ExceptionFilter>();
            services.AddTransient<ModelStateFilter>();

            return services;
        }
    }
}
