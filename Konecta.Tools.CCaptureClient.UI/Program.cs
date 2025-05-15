using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Konecta.Tools.CCaptureClient.Infrastructure.Services;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.UI.Forms;

namespace Konecta.Tools.CCaptureClient.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize application configuration
            ApplicationConfiguration.Initialize();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

            // Set up dependency injection
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Run the application
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<IApiDatabaseService, ApiDatabaseService>();
            services.AddTransient<MainForm>();
        }
    }
}
