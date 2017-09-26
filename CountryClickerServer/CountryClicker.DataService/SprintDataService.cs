using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class SprintDataService : DataService<Sprint, Guid>
    {
        public SprintDataService(CountryClickerDbContext context) : base(context) { }

        public override Sprint Get(Guid id) => Context.Sprints.Find(id);
        public override IQueryable<Sprint> GetMany() => Context.Sprints;
        public override IQueryable<Sprint> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Sprints.
            FromSql($"SELECT * FROM Sprints WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(Sprint instance) => true;
    }
}
