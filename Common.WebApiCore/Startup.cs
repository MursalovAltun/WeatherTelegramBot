using System.Reflection;
using AutoMapper;
using Common.Services.Infrastructure;
using Common.WebApiCore.Middlewares;
using Common.WebApiCore.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.WebApiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        protected void ConfigureDependencies(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            DependenciesConfig.ConfigureDependencies(services, Configuration, connectionString);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDependencies(services);
            services.AddControllers(options =>
            {
                options.UseCentralRoutePrefix(new RouteAttribute("api"));
            });
            services.ConfigureSwagger();
            services.ConfigureCors();
            services.AddAutoMapper(Assembly.Load("Common.Services.Infrastructure"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDataBaseInitializer dataBaseInitializer)
        {
            if (dataBaseInitializer != null)
            {
                dataBaseInitializer.Initialize();
            }
            else
            {
                // TODO: add logging
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseConfiguredSwagger();

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}