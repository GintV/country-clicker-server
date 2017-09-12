using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryClicker.Domain
{
    public class Continent : Group
    {
        // Navigation properties
        [InverseProperty(nameof(Country.Continent))]
        public Country[] ContinentCountries { get; set; }
    }
}
