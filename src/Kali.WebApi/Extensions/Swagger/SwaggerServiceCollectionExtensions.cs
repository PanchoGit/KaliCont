using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Kali.WebApi.Extensions.Swagger
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultSwagger(this IServiceCollection services, Assembly assembly, string version)
        {
            string projectName = assembly.GetName().Name;

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = projectName, Version = version });
                config.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "Please fill with JWT including the schema (e.g.: Bearer QZ7MltrolBpLMskRV1X...)",
                    Name = "Authorization",
                    Type = "apiKey"
                });
            });

            return services;
        }
    }
}