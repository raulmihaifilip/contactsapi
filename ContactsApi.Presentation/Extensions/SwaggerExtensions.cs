using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
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

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Token based authentication",
                    Description = "Enter jwt bearer token only",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, new string[] { } } };

                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                options.AddSecurityRequirement(securityRequirement);

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
