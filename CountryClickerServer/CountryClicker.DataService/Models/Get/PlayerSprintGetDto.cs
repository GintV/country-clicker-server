using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class PlayerSprintGetDto : IGetDto<PlayerSprint, Guid[]>
    {
        public Guid[] Id { get => new[] { PlayerId, SprintId }; }
        public long Score { get; set; }
        public Guid PlayerId { get; set; }
        public Guid SprintId { get; set; }
    }
}
