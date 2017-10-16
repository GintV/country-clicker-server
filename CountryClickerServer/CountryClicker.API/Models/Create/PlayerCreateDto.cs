using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class PlayerCreateDto : ICreateDto<Player>
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        public Guid? UserId { get; set; }
    }

    public class PlayerParentableCreateDto : IParentableCreateDto<Player, Guid>
    {
        [Required]
        public string Nickname { get; set; }
        public Guid? UserId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => UserId = parentId;
    }
}
