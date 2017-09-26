using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryClicker.DataService;
using CountryClicker.Domain;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services) => services.
            AddScoped<IDataService<City, Guid>, CityDataService>().
            AddScoped<IDataService<Continent, Guid>, ContinentDataService>().
            AddScoped<IDataService<Country, Guid>, CountryDataService>().
            AddScoped<IDataService<CustomGroup, Guid>, CustomGroupDataService>().
            AddScoped<IDataService<GroupSprint, Guid[]>, GroupSprintDataService>().
            AddScoped<IDataService<Player, Guid>, PlayerDataService>().
            AddScoped<IDataService<PlayerSprint, Guid[]>, PlayerSprintDataService>().
            AddScoped<IDataService<PlayerSubscription, Guid[]>, PlayerSubscriptionDataService>().
            AddScoped<IDataService<Sprint, Guid>, SprintDataService>().
            AddScoped<IDataService<User, Guid>, UserDataService>();
    }
}
