using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class PlayerSprintUpdateDto : IUpdateDto<PlayerSprint>
    {
        [Required]
        public long Score { get; set; }
    }
}
