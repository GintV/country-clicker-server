using CountryClicker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountryClicker.DataService.Models.Create
{
    public class PlayerUpdateDto : IUpdateDto<Player>
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        public Guid? UserId { get; set; }
    }
}
