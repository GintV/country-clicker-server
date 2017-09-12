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
        public ulong Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Guid GroupId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [InverseProperty(nameof(PlayerSprint.Sprint))]
        public PlayerSprint PlayerSprints { get; set; }
    }
}
