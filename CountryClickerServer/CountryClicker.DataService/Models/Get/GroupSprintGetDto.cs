using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class GroupSprintGetDto : IGetDto<GroupSprint, Guid[]>
    {
        public Guid[] Id { get => new[] { GroupId, SprintId }; }
        public long Score { get; set; }
        public Guid GroupId { get; set; }
        public Guid SprintId { get; set; }
    }
}
