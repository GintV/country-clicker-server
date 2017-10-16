using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class GroupSprintGetDto : IGetDto<GroupSprint, Guid[]>
    {
        public Guid[] Id { get => new[] { GroupId, SprintId }; }
        public long Score { get; set; }
        public Guid GroupId { get; set; }
        public Guid SprintId { get; set; }
    }
}
