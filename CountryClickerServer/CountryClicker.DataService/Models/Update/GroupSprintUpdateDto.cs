using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class GroupSprintUpdateDto : IUpdateDto<GroupSprint>
    {
        [Required]
        public Guid? GroupId { get; set; }
        [Required]
        public Guid? SprintId { get; set; }
    }
}
