using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class PlayerSprintGetDto : IGetDto<PlayerSprint, Guid[]>
    {
        public Guid[] Id { get => new[] { PlayerId, SprintId }; }
        public long Score { get; set; }
        public Guid PlayerId { get; set; }
        public Guid SprintId { get; set; }
    }
}
