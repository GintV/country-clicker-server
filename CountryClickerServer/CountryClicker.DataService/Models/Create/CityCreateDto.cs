using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class CityCreateDto : ICreateDto<City>
    {
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual Guid? CountryId { get; set; }
    }

    public class CityParentableCreateDto : IParentableCreateDto<City, Guid>
    {
        [Required]
        public virtual string Title { get; set; }
        public virtual Guid? CountryId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => CountryId = parentId;
    }
}
