using Common.DIContainerCore;
using Common.WebApiCore.Middlewares;
using Common.WebApiCore.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            //var connectionString = Configuration.GetConnectionString("localDb");
            DependenciesConfig.ConfigureDependencies(services, Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDependencies(services);
            services.ConfigureSwagger();
            services.ConfigureCors();

            services.AddMvc(options =>
            {
                options.UseCentralRoutePrefix(new RouteAttribute("api"));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseMvc();
        }
    }
}