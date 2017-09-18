using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CountryClicker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using CountryClicker.Domain;
using CountryClicker.DataService.Models.Create;
using CountryClicker.DataService.Models.Get;

namespace CountryClicker.API
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
            services.AddDbContext<CountryClickerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CountryClickerPublish"));
            });
            services.AddDataServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<Continent, ContinentGetDto>();
                configuration.CreateMap<Country, CountryGetDto>();
                configuration.CreateMap<ContinentCreateDto, Continent>();
                configuration.CreateMap<CountryCreateDto, Country>();
            });

            app.UseMvc();
        }
    }
}
