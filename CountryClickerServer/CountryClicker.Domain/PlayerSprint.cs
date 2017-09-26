using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class PlayerSprint : IParentableEntity<Guid>
    {
        // Table columns
        public long Score { get; set; }
        public Guid PlayerId { get; set; }
        public Guid SprintId { get; set; }

        // Navigaiton properties
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [ForeignKey(nameof(SprintId))]
        public Sprint Sprint { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => parentEntityName == nameof(Domain.Player) ? PlayerId : SprintId;
    }
}
