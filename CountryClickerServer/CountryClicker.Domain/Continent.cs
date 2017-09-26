using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Continent : Group, IEntity
    {
        // Navigation properties
        [InverseProperty(nameof(Country.Continent))]
        public ICollection<Country> ContinentCountries { get; set; }
    }
}
