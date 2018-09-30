using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OventService;

namespace OventApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);
            app.UseMvc();
        }
    }
}
