using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class SprintUpdateDto : IUpdateDto<Sprint>
    {
        [Required]
        public long Score { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}
