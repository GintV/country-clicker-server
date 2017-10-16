using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class GroupSprintUpdateDto : IUpdateDto<GroupSprint>
    {
        public long Score { get; set; }
    }
}
