using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AssemblyWorker>();
        }
    }
}
