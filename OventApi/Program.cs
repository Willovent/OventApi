using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace OventApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5001, listenOptions =>
                      {
                          listenOptions.UseHttps("https.pfx", "P@ssw0rd");
                      });
                    options.Listen(IPAddress.Any, 5000);
                })
                .Build();
    }
}
