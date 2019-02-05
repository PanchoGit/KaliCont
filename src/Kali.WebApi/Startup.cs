using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Kali.Security;
using Kali.WebApi.Filters;
using Kali.WebApi.Infrastructures;

namespace Kali.WebApi
{
    public class Startup
    {
        private SecurityManager securityManager;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddOptions();

            services.Configure<TokenSetting>(Configuration.GetSection(nameof(TokenSetting)));

            services.AddMvc(options =>
            {
                options.Filters.Add(new VersionHeaderFilter());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = securityManager.TokenValidationParameters;
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DefaultModule { Configuration = Configuration });

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            var setting = new TokenSetting();

            Configuration.Bind(nameof(TokenSetting), setting);

            securityManager = new SecurityManager(setting);

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(b => b.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders(VersionHeaderFilter.HeaderAppVersionName));
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            ConfigureLogger();
        }

        private void ConfigureLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            var log4NetConfig = Configuration.GetSection("log4NetConfig").Value;

            XmlConfigurator.Configure(logRepository, new FileInfo(log4NetConfig));
        }
    }
}
