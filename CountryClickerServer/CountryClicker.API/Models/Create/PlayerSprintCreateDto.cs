using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class PlayerSprintCreateDto : ICreateDto<PlayerSprint>
    {
        [Required]
        public Guid? PlayerId { get; set; }
        [Required]
        public Guid? SprintId { get; set; }
    }

    public class PlayerSprintFromPlayerParentableCreateDto : IParentableCreateDto<PlayerSprint, Guid>
    {
        public Guid? PlayerId { get; set; }
        [Required]
        public Guid? SprintId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => PlayerId = parentId;
    }

    public class PlayerSprintFromSprintParentableCreateDto : IParentableCreateDto<PlayerSprint, Guid>
    {
        [Required]
        public Guid? PlayerId { get; set; }
        public Guid? SprintId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => SprintId = parentId;
    }
}
