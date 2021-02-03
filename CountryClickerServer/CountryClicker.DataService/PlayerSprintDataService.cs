using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CountryClicker.DataService
{
    // ReSharper disable once ClassNeverInstantiated.Global, reason: dependency injection
    public class PlayerSprintDataService : DataService<PlayerSprint, Guid[]>
    {
        public PlayerSprintDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(PlayerSprint instance) { }
        public override PlayerSprint Get(Guid[] id) => Context.PlayerSprints.Find(id[0], id[1]);
        public override IQueryable<PlayerSprint> GetMany() => Context.PlayerSprints;
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<PlayerSprint> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.PlayerSprints.
            FromSql($"SELECT * FROM PlayerSprints WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(PlayerSprint instance) =>
            Context.Players.Find(instance.PlayerId) != null ? (Context.Sprints.Find(instance.SprintId) != null, instance.SprintId.ToString()) :
            (false, instance.PlayerId.ToString());
    }
}
