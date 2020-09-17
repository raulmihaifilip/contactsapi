using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ContactsApi.Presentation.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddContactsApiSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Contacts API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                var security = new OpenApiSecurityRequirement() { { securityScheme, new List<string>() } };

                options.AddSecurityRequirement(security);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SchemaFilter<SchemaFilter>();
            });

            return services;
        }
        public static IApplicationBuilder UseContactsApiSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1"));

            return app;
        }

        class SchemaFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                if (schema.Properties == null)
                {
                    return;
                }

                foreach (var property in schema.Properties)
                {
                    var propertyType = property.Value.Type;
                    if (property.Value.Example == null && !string.IsNullOrEmpty(propertyType))
                    {
                        if (propertyType.ToLower().Equals(typeof(string).Name.ToLower()))
                        {
                            property.Value.Example = property.Value.Default ?? new OpenApiString(string.Empty);
                        }
                    }
                }
            }
        }
    }
}
