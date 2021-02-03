using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class GroupSprintUpdateDto : IUpdateDto<GroupSprint>
    {
        [Required]
        public long Score { get; set; }
    }
}
