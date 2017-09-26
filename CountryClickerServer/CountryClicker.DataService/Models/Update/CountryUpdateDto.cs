using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class CountryUpdateDto : IUpdateDto<Country>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid? ContinentId { get; set; }
    }
}
