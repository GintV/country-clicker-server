using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CountryClicker.Domain
{
    public class GroupSprint : IParentableEntity<Guid>
    {
        // Table columns
        public long Score { get; set; }
        public Guid GroupId { get; set; }
        public Guid SprintId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [ForeignKey(nameof(SprintId))]
        public Sprint Sprint { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => parentEntityName == nameof(Domain.Group) ? GroupId : SprintId;
    }
}
