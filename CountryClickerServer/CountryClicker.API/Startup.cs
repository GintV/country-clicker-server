using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Update;
using CountryClicker.Data;
using CountryClicker.Domain;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using static CountryClicker.API.Constants;

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
            services.AddCors(setup => setup.AddPolicy("AllowMyClient", configure =>
            {
                configure.WithOrigins("https://localhost:44389").AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
            }));
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:44373/";
                options.RequireHttpsMetadata = true;
                options.ApiName = "countryclickerapi";
                options.ApiSecret = "apisecret";
            });
            services.AddMvc(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setup.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            });
            services.AddDbContext<CountryClickerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CountryClicker"));
            });
            services.AddDataServices();
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(ApiVerionString, new Info { Title = "CountryClicker API", Version = ApiVerionString });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            AutoMapper.Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<City, CityGetDto>();
                configuration.CreateMap<CityCreateDto, City>();
                configuration.CreateMap<CityParentableCreateDto, City>();
                configuration.CreateMap<CityUpdateDto, City>();

                configuration.CreateMap<Continent, ContinentGetDto>();
                configuration.CreateMap<ContinentCreateDto, Continent>();
                configuration.CreateMap<ContinentUpdateDto, Continent>();

                configuration.CreateMap<Country, CountryGetDto>();
                configuration.CreateMap<CountryCreateDto, Country>();
                configuration.CreateMap<CountryParentableCreateDto, Country>();
                configuration.CreateMap<CountryUpdateDto, Country>();

                configuration.CreateMap<CustomGroup, CustomGroupGetDto>();
                configuration.CreateMap<CustomGroupCreateDto, CustomGroup>();
                configuration.CreateMap<CustomGroupParentableCreateDto, CustomGroup>();
                configuration.CreateMap<CustomGroupUpdateDto, CustomGroup>();

                configuration.CreateMap<GroupSprint, GroupSprintGetDto>();
                configuration.CreateMap<GroupSprintCreateDto, GroupSprint>();
                configuration.CreateMap<GroupSprintFromGroupParentableCreateDto, GroupSprint>();
                configuration.CreateMap<GroupSprintFromSprintParentableCreateDto, GroupSprint>();

                configuration.CreateMap<Player, PlayerGetDto>();
                configuration.CreateMap<PlayerCreateDto, Player>();
                configuration.CreateMap<PlayerUpdateDto, Player>();

                configuration.CreateMap<PlayerSprint, PlayerSprintGetDto>();
                configuration.CreateMap<PlayerSprintCreateDto, PlayerSprint>();
                configuration.CreateMap<PlayerSprintFromPlayerParentableCreateDto, GroupSprint>();
                configuration.CreateMap<PlayerSprintFromSprintParentableCreateDto, GroupSprint>();

                configuration.CreateMap<PlayerSubscription, PlayerSubscriptionGetDto>();
                configuration.CreateMap<PlayerSubscriptionCreateDto, PlayerSubscription>();
                configuration.CreateMap<PlayerSubscriptionFromPlayerParentableCreateDto, PlayerSubscription>();
                configuration.CreateMap<PlayerSubscriptionFromGroupParentableCreateDto, PlayerSubscription>();

                configuration.CreateMap<Sprint, SprintGetDto>();
                configuration.CreateMap<SprintCreateDto, Sprint>();

            });

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint(SwaggerEndpoint, "CountryClicker API");
            });
        }
    }
}
