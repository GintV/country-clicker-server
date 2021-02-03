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
    public class PlayerDataService : DataService<Player, Guid>
    {
        public PlayerDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(Player instance) { }
        public override Player Get(Guid id) => Context.Players.Find(id);
        public override IQueryable<Player> GetMany() => Context.Players.OrderByDescending(res => res.Score);
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<Player> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Players.
            FromSql($"SELECT * FROM Players WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(Player instance) => (true, null);
    }
}
