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
            AddScoped<IDataService<Continent, Guid>, ContinentDataService>().
            AddScoped<IDataService<Country, Guid>, CountryDataService>();
    }
}
