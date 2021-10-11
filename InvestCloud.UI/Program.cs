using InvestCloud.App.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InvestCloud.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var section = builder.Build().GetSection("Endpoints:Main");

            Log.Logger.Information("Application starting:");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    services
                        .AddTransient<IMatrixService, ParallelMatrixService>()
                        .AddHttpClient<INumbersClient, NumbersClient>(c => c.BaseAddress = new Uri(section.Value));

                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<ParallelMatrixService>(host.Services);
            await svc.Run(1000);

        }

        static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
