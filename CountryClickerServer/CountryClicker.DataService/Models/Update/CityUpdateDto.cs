using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class CityUpdateDto : IUpdateDto<City>
    {
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual Guid? CountryId { get; set; }
    }
}
