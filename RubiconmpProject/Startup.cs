using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RubiconmpProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace RubiconmpProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
         => services.AddDbContext<RepositoryContext>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
