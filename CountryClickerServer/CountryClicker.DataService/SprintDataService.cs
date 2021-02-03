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
    public class SprintDataService : DataService<Sprint, Guid>
    {
        public SprintDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(Sprint instance) { }
        public override Sprint Get(Guid id) => Context.Sprints.Find(id);
        public override IQueryable<Sprint> GetMany() => Context.Sprints;
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<Sprint> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Sprints.
            FromSql($"SELECT * FROM Sprints WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(Sprint instance) => (true, null);
    }
}
