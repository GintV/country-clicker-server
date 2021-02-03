using System;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Get
{
    public class PlayerGetDto : IGetDto<Player, Guid>
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public long Score { get; set; }
        public string UserId { get; set; }
    }
}
