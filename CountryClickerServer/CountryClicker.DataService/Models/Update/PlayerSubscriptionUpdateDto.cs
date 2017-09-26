using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class PlayerSubscriptionUpdateDto : IUpdateDto<PlayerSubscription>
    {
        [Required]
        public DateTime SubscribeTime { get; set; } = DateTime.Now;
        [Required]
        public Guid? PlayerId { get; set; }
        [Required]
        public Guid? GroupId { get; set; }
    }
}
