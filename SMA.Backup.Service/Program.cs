using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SMA.Backup.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    Runtime.Startup.ConfigurationBuilder(builder);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddEventLog((setting) =>
                    {
                        setting.SourceName = "SMA.Backup.Service";
                        setting.LogName = "Application";
                    });
                });
    }
}
