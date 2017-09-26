using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class ContinentUpdateDto : IUpdateDto<Continent>
    {
        [Required]
        public string Title { get; set; }
    }
}
