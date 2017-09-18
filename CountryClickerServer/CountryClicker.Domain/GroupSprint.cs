using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.Domain
{
    public class GroupSprint : IEntity
    {
        // Table columns
        public Guid Id { get; set; }
        public long Score { get; set; }
        public Guid GroupId { get; set; }
        public Guid SprintId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [ForeignKey(nameof(SprintId))]
        public Sprint Sprint { get; set; }
    }
}
