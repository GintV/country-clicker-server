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
    public class PlayerSubscriptionDataService : DataService<PlayerSubscription, Guid[]>
    {
        public PlayerSubscriptionDataService(CountryClickerDbContext context) : base(context) { }

        public override void DeleteReferences(PlayerSubscription instance) { }
        public override PlayerSubscription Get(Guid[] id) => Context.PlayerSubscriptions.Find(id[0], id[1]);
        public override IQueryable<PlayerSubscription> GetMany() => Context.PlayerSubscriptions;
        // ReSharper disable once RedundantToStringCall, reason: different method overload
        public override IQueryable<PlayerSubscription> GetManyFilter(params (string column, string value)[] columnValuePairs) => Context.
            PlayerSubscriptions.FromSql($"SELECT * FROM PlayerSubscriptions WHERE {CombineFilter(columnValuePairs)}".ToString());
        public override (bool IsValid, string NotFoundParentId) AreRelationshipsValid(PlayerSubscription instance) =>
            Context.Players.Find(instance.PlayerId) != null ? (Context.Find(typeof(Group), instance.GroupId) != null, instance.GroupId.ToString()) :
            (false, instance.PlayerId.ToString());
    }
}
