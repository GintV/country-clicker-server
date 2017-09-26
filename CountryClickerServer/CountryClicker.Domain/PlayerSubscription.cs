using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class PlayerSubscription : IParentableEntity<Guid>
    {
        // Table columns
        public DateTime SubscribeTime { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => parentEntityName == nameof(Domain.Player) ? PlayerId : GroupId;
    }
}