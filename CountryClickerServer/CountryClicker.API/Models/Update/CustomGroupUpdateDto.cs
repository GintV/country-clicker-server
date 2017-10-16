using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class CustomGroupUpdateDto : IUpdateDto<CustomGroup>
    {
        [Required]
        public string Title { get; set; }
        public long Score { get; set; }

        [Required]
        public Guid? CreatedBy { get; set; }
    }
}
