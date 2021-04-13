using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace ACompany.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                 //   webBuilder.UseUrls("https://localhost:44317/");
                });
    }
}
