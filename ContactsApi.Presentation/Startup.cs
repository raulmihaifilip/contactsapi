using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContactsApi.Core.Models;
using FluentValidation.AspNetCore;
using ContactsApi.Presentation.Extensions;
using ContactsApi.Presentation.Filters;

namespace Contacts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddContactsApiSwagger();

            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
            var authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>();
            services.AddContactsApiAuthentication(authOptions);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddContactsApiPersistence(connectionString);
            services.AddContactsApiCore();
            services.AddPresentationExtensions();

            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ModelStateFilter>();
            }).AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
              .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseContactsApiSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}