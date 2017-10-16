using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class PlayerSprintUpdateDto : IUpdateDto<PlayerSprint>
    {
        public long Score { get; set; }
    }
}
