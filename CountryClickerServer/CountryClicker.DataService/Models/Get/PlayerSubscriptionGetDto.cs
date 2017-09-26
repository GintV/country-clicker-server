using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class PlayerSubscriptionGetDto : IGetDto<PlayerSubscription, Guid[]>
    {
        public Guid[] Id { get => new[] { PlayerId, GroupId }; }
        public long Score { get; set; }
        public DateTime SubscribeTime { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }
    }
}
