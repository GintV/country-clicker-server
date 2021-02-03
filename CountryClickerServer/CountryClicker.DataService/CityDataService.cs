using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    // ReSharper disable once ClassNeverInstantiated.Global, reason: dependency injection
    public class CityDataService : DataService<City, Guid>
    {
        public CityDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(City instance) { }
        public override City Get(Guid id) => Context.Cities.Find(id);
        public override IQueryable<City> GetMany() => Context.Cities;
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<City> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Cities.
            FromSql($"SELECT * FROM dbo.[Group] WHERE Discriminator = 'City' AND {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(City instance) => (Context.Countries.Find(instance.CountryId) != null,
            instance.CountryId.ToString());
    }
}
