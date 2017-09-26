using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class ContinentDataService : DataService<Continent, Guid>
    {
        public ContinentDataService(CountryClickerDbContext context) : base(context) { }

        public override Continent Get(Guid id) => Context.Continents.Find(id);
        public override IQueryable<Continent> GetMany() => Context.Continents;
        public override IQueryable<Continent> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Continents.
            FromSql($"SELECT * FROM dbo.[Group] WHERE Discriminator = 'Continent' AND {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(Continent instance) => true;
    }
}
