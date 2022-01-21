using BLL.Abstractions.Interfaces;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ITestRunner), typeof(TestRunner));

            DAL.DependencyRegistrar.ConfigureServices(services);
        }
    }
}
