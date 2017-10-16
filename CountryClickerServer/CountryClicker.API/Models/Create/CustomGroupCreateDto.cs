using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class CustomGroupCreateDto : ICreateDto<CustomGroup>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid? CreatedBy { get; set; }
    }

    public class CustomGroupParentableCreateDto : IParentableCreateDto<CustomGroup, Guid>
    {
        [Required]
        public string Title { get; set; }
        public Guid? CreatedBy { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => CreatedBy = parentId;
    }
}
