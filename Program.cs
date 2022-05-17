using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace HarperNetClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
           
            ConfigureServices(services);
        }

        private static void ConfigureServices(ServiceCollection service)
        {
            var path = Directory.GetCurrentDirectory();
        }
    }
}
