using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Abstractions.Interfaces;
using BLL.Services;

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
