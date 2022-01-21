using System;
using System.Reflection;
using Core;
using Microsoft.Extensions.Options;

namespace DAL
{
    public class AssemblyWorker
    {
        private readonly AppSettings _appSettings;

        public AssemblyWorker(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public Assembly GetTestingAssembly()
        {
            return Assembly.LoadFrom(_appSettings.TestingAssemblyPath);
        }
    }
}
