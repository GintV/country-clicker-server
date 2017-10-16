using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class CityCreateDto : ICreateDto<City>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid? CountryId { get; set; }
    }

    public class CityParentableCreateDto : IParentableCreateDto<City, Guid>
    {
        [Required]
        public string Title { get; set; }
        public Guid? CountryId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => CountryId = parentId;
    }
}
