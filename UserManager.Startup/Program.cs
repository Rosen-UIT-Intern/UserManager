using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace UserManager.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;

            return
            WebHost.CreateDefaultBuilder(args)
                //.UseStartup<Startup>()
                .ConfigureLogging((WebHostBuilderContext ctx, ILoggingBuilder builder) =>
                   {
                       builder.AddConfiguration(ctx.Configuration.GetSection("Logging"));
                       builder.AddFile(o => o.RootPath = AppContext.BaseDirectory);
                   })
                .UseStartup(assemblyName)
                ;
        }
    }
}
