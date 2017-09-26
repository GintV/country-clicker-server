using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.Domain
{
    public class City : Group, IParentableEntity<Guid>
    {
        // Table columns
        public Guid CountryId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => CountryId;
    }
}
