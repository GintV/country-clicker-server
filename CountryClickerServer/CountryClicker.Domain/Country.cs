using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Country : Group, IParentableEntity<Guid>
    {
        // Table columns
        public Guid ContinentId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ContinentId))]
        public Continent Continent { get; set; }

        [InverseProperty(nameof(City.Country))]
        public ICollection<City> CountryCities { get; set; }

        // Interface realization
        public Guid ParentId(string parentEntityName) => ContinentId;
    }
}
