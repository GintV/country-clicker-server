using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class CountryCreateDto : ICreateDto<Country>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid? ContinentId { get; set; }
    }

    public class CountryParentableCreateDto : IParentableCreateDto<Country, Guid>
    {
        [Required]
        public string Title { get; set; }
        public Guid? ContinentId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => ContinentId = parentId;
    }
}
