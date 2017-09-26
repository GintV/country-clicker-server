using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryClicker.DataService.Models.Get
{
    public class PlayerGetDto : IGetDto<Player, Guid>
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public long Score { get; set; }
        public Guid UserId { get; set; }
    }
}
