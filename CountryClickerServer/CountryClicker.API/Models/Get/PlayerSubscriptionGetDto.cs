using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class PlayerSubscriptionGetDto : IGetDto<PlayerSubscription, Guid[]>
    {
        public Guid[] Id { get => new[] { PlayerId, GroupId }; }
        public DateTime? SubscribeTime { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }
    }
}
