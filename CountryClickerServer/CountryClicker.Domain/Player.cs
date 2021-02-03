using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Player : IEntity
    {
        // Table columns
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public long Score { get; set; }
        public string UserId { get; set; }

        // Navigation properties
        [InverseProperty(nameof(PlayerSubscription.Player))]
        public ICollection<PlayerSubscription> SubscribedGroups { get; set; }

        [InverseProperty(nameof(CustomGroup.CreatedBy))]
        public ICollection<CustomGroup> CreatedCustomGroups { get; set; }
    }
}