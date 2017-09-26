using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class GroupSprintDataService : DataService<GroupSprint, Guid[]>
    {
        public GroupSprintDataService(CountryClickerDbContext context) : base(context) { }

        public override GroupSprint Get(Guid[] id) => Context.GroupSprints.Find(id[0], id[1]);
        public override IQueryable<GroupSprint> GetMany() => Context.GroupSprints;
        public override IQueryable<GroupSprint> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.GroupSprints.
            FromSql($"SELECT * FROM GroupSprints WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(GroupSprint instance) => Context.Find(typeof(Group), instance.GroupId) != null &&
            Context.Sprints.Find(instance.SprintId) != null;
            
    }
}
