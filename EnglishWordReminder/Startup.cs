using EnglishWordReminder.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishWordReminder
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(Configuration);

            ConfigureApplicationServices(services);
            IServiceProvider provider = services.BuildServiceProvider();

            return provider;
        }

        public void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddSingleton<BotManager>();
        }
    }
}
