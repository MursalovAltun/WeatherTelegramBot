using Common.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Common.WebApiCore.Setup
{
    public static class QuartzConfig
    {
        public static void ConfigureQuartz(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<DailyWeatherJob>();

            services.AddSingleton(new JobSchedule(
                jobType: typeof(DailyWeatherJob),
                cronExpression: "0 0 * ? * *"));

            services.AddHostedService<QuartzHostedService>();
        }
    }
}