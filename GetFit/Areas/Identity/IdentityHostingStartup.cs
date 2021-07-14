using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GetFit.Areas.Identity.IdentityHostingStartup))]
namespace GetFit.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}