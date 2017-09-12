using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class PlayerSubscription
    {
        // Table columns
        public Guid Id { get; set; }
        public DateTime SubscribeTime { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GroupId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }
    }
}