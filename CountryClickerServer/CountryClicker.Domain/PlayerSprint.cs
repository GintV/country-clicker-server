using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class PlayerSprint : IEntity
    {
        // Table columns
        public Guid Id { get; set; }
        public long Score { get; set; }
        public Guid PlayerId { get; set; }
        public Guid SprintId { get; set; }

        // Navigaiton properties
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [ForeignKey(nameof(SprintId))]
        public Sprint Sprint { get; set; }
    }
}
