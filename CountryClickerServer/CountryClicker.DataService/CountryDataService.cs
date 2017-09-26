using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class CountryDataService : DataService<Country, Guid>
    {
        public CountryDataService(CountryClickerDbContext context) : base(context) { }

        public override Country Get(Guid id) => Context.Countries.Find(id);
        public override IQueryable<Country> GetMany() => Context.Countries;
        public override IQueryable<Country> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Countries.
            FromSql($"SELECT * FROM dbo.[Group] WHERE Discriminator = 'Country' AND {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(Country instance) => Context.Continents.Find(instance.ContinentId) != null;
    }
}
