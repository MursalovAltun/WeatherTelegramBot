using Common.DIContainerCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApiCore.Setup
{
    public class DependenciesConfig
    {
        public static void ConfigureDependencies(IServiceCollection services, IConfiguration configuration, string connectionString = null)
        {
            services.AddHttpContextAccessor();

            ContainerExtension.Initialize(services, configuration, connectionString);
        }
    }
}