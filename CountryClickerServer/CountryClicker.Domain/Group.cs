using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public abstract class Group
    {
        // Table columns
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ulong Score { get; set; }

        // Navigation properties
        [InverseProperty(nameof(PlayerSubscription.Group))]
        public PlayerSubscription[] GroupSubscribers { get; set; }
    }
}