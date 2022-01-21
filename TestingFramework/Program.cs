using System;
using System.IO;
using BLL.Services;
using Core;
using Core.Models;
using Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<App>().StartApp();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddScoped<App>();

            BLL.DependencyRegistrar.ConfigureServices(services);
        }
    }
}
