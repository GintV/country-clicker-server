using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Update
{
    public class PlayerUpdateDto : IUpdateDto<Player>
    {
        [Required]
        public string Nickname { get; set; }
        public long Score { get; set; }
        [Required]
        public Guid? UserId { get; set; }
    }
}
