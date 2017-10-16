using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.API.Models.Create
{
    public class ContinentCreateDto : ICreateDto<Continent>
    {
        [Required]
        public string Title { get; set; }
    }
}
