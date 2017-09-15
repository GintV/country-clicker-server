using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.Domain
{
    public class Sprint
    {
        // Table columns
        public Guid Id { get; set; }
        public long Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        // Navigation properties
        [InverseProperty(nameof(PlayerSprint.Sprint))]
        public PlayerSprint[] PlayerSprints { get; set; }

        [InverseProperty(nameof(GroupSprint.Sprint))]
        public GroupSprint[] GroupSprints { get; set; }
    }
}
