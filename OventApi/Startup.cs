using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OventService;

namespace OventApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped((provider) =>
                 new OventBusinessService(
                     Configuration["OventServiceConfiguration:zwayApiHost"],
                     Configuration["OventServiceConfiguration:kodiHost"],
                     Configuration["OventServiceConfiguration:kodiUserName"],
                     Configuration["OventServiceConfiguration:KodiPassword"])
            );
            services.AddScoped((p) => new KodiService(Configuration["OventServiceConfiguration:kodiHost"],80, Configuration["OventServiceConfiguration:kodiUserName"], Configuration["OventServiceConfiguration:KodiPassword"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMvc();
        }
    }
}
