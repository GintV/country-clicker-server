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
    public class ContinentDataService : DataService<Continent, Guid>
    {
        public ContinentDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(Continent instance) { }
        public override Continent Get(Guid id) => Context.Continents.Find(id);
        public override IQueryable<Continent> GetMany() => Context.Continents.OrderByDescending(res => res.Score);
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<Continent> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Continents.
            FromSql($"SELECT * FROM dbo.[Group] WHERE Discriminator = 'Continent' AND {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(Continent instance) => (true, null);
    }
}
