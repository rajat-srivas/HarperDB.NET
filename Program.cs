using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HarperNetClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var logger = new LoggerFactory();
            ConfigureServices(services);
        }

        private static void ConfigureServices(ServiceCollection service)
        {
            var path = Directory.GetCurrentDirectory();
            var seriLogger = new LoggerConfiguration().WriteTo.File($"{path}/Logs/log_{DateTime.Now.Date}.txt").CreateLogger();
            service.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog(logger: seriLogger, dispose: true);
            });
        }
    }
}
