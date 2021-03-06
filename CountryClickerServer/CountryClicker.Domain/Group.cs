﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public abstract class Group
    {
        // Table columns
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Score { get; set; }

        // Navigation properties
        [InverseProperty(nameof(PlayerSubscription.Group))]
        public ICollection<PlayerSubscription> GroupSubscribers { get; set; }
    }
}