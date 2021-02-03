using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class CustomGroupUpdateDto : IUpdateDto<CustomGroup>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public long Score { get; set; }

        [Required]
        public Guid? CreatedById { get; set; }
    }
}
