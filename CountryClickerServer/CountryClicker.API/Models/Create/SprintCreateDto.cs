using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class SprintCreateDto : ICreateDto<Sprint>
    {
        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;
    }
}
