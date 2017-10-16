using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class PlayerSubscriptionUpdateDto : IUpdateDto<PlayerSubscription>
    {
        public long Score { get; set; }
        public DateTime? SubscribeTime { get; set; }
    }
}
