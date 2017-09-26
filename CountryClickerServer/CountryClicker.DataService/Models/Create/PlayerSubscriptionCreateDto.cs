using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class PlayerSubscriptionCreateDto : ICreateDto<PlayerSubscription>
    {
        [Required]
        public DateTime SubscribeTime { get; set; } = DateTime.Now;
        [Required]
        public Guid? PlayerId { get; set; }
        [Required]
        public Guid? GroupId { get; set; }
    }

    public class PlayerSubscriptionFromPlayerParentableCreateDto : IParentableCreateDto<PlayerSubscription, Guid>
    {
        [Required]
        public DateTime SubscribeTime { get; set; } = DateTime.Now;
        public Guid? PlayerId { get; set; }
        [Required]
        public Guid? GroupId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => PlayerId = parentId;
    }

    public class PlayerSubscriptionFromGroupParentableCreateDto : IParentableCreateDto<PlayerSubscription, Guid>
    {
        [Required]
        public DateTime SubscribeTime { get; set; } = DateTime.Now;
        [Required]
        public Guid? PlayerId { get; set; }
        public Guid? GroupId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => GroupId = parentId;
    }
}
