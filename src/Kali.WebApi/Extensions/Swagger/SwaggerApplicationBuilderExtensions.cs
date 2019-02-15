using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Kali.WebApi.Extensions.Swagger
{
    public static class SwaggerApplicationBuilderExtensions
    {
        public static void UseDefaultSwagger(this IApplicationBuilder app, string version)
        {
            IHostingEnvironment env = app.ApplicationServices.GetService<IHostingEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
            }
            else
            {
                app.UseSwagger(config =>
                {
                    var schemes = new[] { "https" };
                    config.PreSerializeFilters.Add((swagger, httpReq) => swagger.Schemes = schemes);
                });
            }

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint($"/swagger/{version}/swagger.json", env.ApplicationName);
            });
        }
    }
}
