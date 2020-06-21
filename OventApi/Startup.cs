using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OventService;

namespace OventApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
            });
            services.AddApplicationInsightsTelemetry();
            services.AddLogging();
            services.AddScoped((provider) =>
                 new OventBusinessService(
                     this.Configuration["OventServiceConfiguration:zwayApiHost"],
                     this.Configuration["OventServiceConfiguration:kodiHost"],
                     this.Configuration["OventServiceConfiguration:kodiUserName"],
                     this.Configuration["OventServiceConfiguration:KodiPassword"])
            );
            services.AddScoped((p) => new KodiService(this.Configuration["OventServiceConfiguration:kodiHost"], 80, this.Configuration["OventServiceConfiguration:kodiUserName"], this.Configuration["OventServiceConfiguration:KodiPassword"]));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
