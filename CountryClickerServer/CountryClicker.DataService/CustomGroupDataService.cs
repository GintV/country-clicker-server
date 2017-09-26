using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class CustomGroupDataService : DataService<CustomGroup, Guid>
    {
        public CustomGroupDataService(CountryClickerDbContext context) : base(context) { }

        public override CustomGroup Get(Guid id) => Context.CustomGroups.Find(id);
        public override IQueryable<CustomGroup> GetMany() => Context.CustomGroups;
        public override IQueryable<CustomGroup> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.CustomGroups.
            FromSql($"SELECT * FROM dbo.[Group] WHERE Discriminator = 'CustomGroup' AND {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(CustomGroup instance) => Context.Players.Find(instance.CreatedById) != null;
    }
}
