using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    // ReSharper disable once ClassNeverInstantiated.Global, reason: dependency injection
    public class CountryDataService : DataService<Country, Guid>
    {
        public CountryDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(Country instance) { }
        public override Country Get(Guid id) => Context.Countries.Find(id);
        public override IQueryable<Country> GetMany() => Context.Countries.OrderByDescending(res => res.Score);
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<Country> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Countries.
            FromSql($"SELECT * FROM dbo.[Group] WHERE Discriminator = 'Country' AND {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(Country instance) =>
            (Context.Continents.Find(instance.ContinentId) != null, instance.ContinentId.ToString());
    }
}
