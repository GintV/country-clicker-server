using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class PlayerDataService : DataService<Player, Guid>
    {
        public PlayerDataService(CountryClickerDbContext context) : base(context) { }

        public override Player Get(Guid id) => Context.Players.Find(id);
        public override IQueryable<Player> GetMany() => Context.Players;
        public override IQueryable<Player> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.Players.
            FromSql($"SELECT * FROM Players WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(Player instance) => Context.Users.Find(instance.UserId) != null;
    }
}
