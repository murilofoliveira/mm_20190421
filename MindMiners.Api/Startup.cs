
using MindMiners.Application.Apps;
using MindMiners.Application.Interface;
using MindMiners.Domain.Interface;
using MindMiners.Domain.Interface.Generic;
using MindMiners.Infra.Repository;
using MindMiners.Infra.Repository.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MindMiners.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddSingleton(typeof(InterfaceGeneric<>), typeof(RepositoryGeneric<>));
            
            services.AddSingleton<InterfaceAirplane, RepositoryAirplane>();
            
            services.AddSingleton<IAppAirplane, ApplicationAirplane>();
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
