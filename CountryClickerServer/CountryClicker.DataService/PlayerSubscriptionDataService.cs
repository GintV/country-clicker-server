using CountryClicker.Data;
using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CountryClicker.DataService
{
    public class PlayerSubscriptionDataService : DataService<PlayerSubscription, Guid[]>
    {
        public PlayerSubscriptionDataService(CountryClickerDbContext context) : base(context) { }

        public override PlayerSubscription Get(Guid[] id) => Context.PlayerSubscriptions.Find(id[0], id[1]);
        public override IQueryable<PlayerSubscription> GetMany() => Context.PlayerSubscriptions;
        public override IQueryable<PlayerSubscription> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.
            PlayerSubscriptions.FromSql($"SELECT * FROM PlayerSubscriptions WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override bool AreRelationshipsValid(PlayerSubscription instance) => Context.Players.Find(instance.PlayerId) != null &&
            Context.Find(typeof(Group), instance.GroupId) != null;
    }
}
