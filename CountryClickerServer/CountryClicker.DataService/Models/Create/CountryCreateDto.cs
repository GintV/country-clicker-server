using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
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
        public virtual string Title { get; set; }
        public virtual Guid? ContinentId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => ContinentId = parentId;
    }
}
