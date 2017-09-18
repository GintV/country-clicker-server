using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class ContinentCreateDto : ICreatableDto
    {
        [Required]
        public string Title { get; set; }
    }
}
