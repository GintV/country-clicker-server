using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class PlayerSubscriptionUpdateDto : IUpdateDto<PlayerSubscription>
    {
        [Required]
        public long Score { get; set; }
        public DateTime? SubscribeTime { get; set; }
    }
}
