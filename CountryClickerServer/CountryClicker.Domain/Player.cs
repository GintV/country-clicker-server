using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Player
    {
        // Table columns
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public ulong Score { get; set; }
        public Guid UserId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [InverseProperty(nameof(PlayerSubscription.Player))]
        public PlayerSubscription[] SubscribedGroups { get; set; }

        [InverseProperty(nameof(CustomGroup.CreatedBy))]
        public CustomGroup[] CreatedCustomGroups { get; set; }
    }
}