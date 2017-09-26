using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Player : IParentableEntity<Guid>
    {
        // Table columns
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public long Score { get; set; }
        public Guid UserId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [InverseProperty(nameof(PlayerSubscription.Player))]
        public ICollection<PlayerSubscription> SubscribedGroups { get; set; }

        [InverseProperty(nameof(CustomGroup.CreatedBy))]
        public ICollection<CustomGroup> CreatedCustomGroups { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => UserId;
    }
}