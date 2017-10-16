using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class CityUpdateDto : IUpdateDto<City>
    {
        [Required]
        public string Title { get; set; }
        public long Score { get; set; }
        [Required]
        public virtual Guid? CountryId { get; set; }
    }
}
