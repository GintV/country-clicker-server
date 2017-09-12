using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Country : Group
    {
        // Table columns
        public Guid ContinentId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ContinentId))]
        public Continent Continent { get; set; }

        [InverseProperty(nameof(City.Country))]
        public City[] CountryCities { get; set; }
    }
}
