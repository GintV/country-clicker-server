using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class SprintUpdateDto : IUpdateDto<Sprint>
    {
        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;
    }
}
