using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
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
        public virtual string Title { get; set; }
        public virtual Guid? CreatedBy { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => CreatedBy = parentId;
    }
}
