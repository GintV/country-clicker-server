using System;
using System.ComponentModel.DataAnnotations;
using CountryClicker.Domain;

namespace CountryClicker.API.Models.Create
{
    public class GroupSprintCreateDto : ICreateDto<GroupSprint>
    {
        [Required]
        public Guid? GroupId { get; set; }
        [Required]
        public Guid? SprintId { get; set; }
    }

    public class GroupSprintFromGroupParentableCreateDto : IParentableCreateDto<GroupSprint, Guid>
    {
        public Guid? GroupId { get; set; }
        [Required]
        public Guid? SprintId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => GroupId = parentId;
    }

    public class GroupSprintFromSprintParentableCreateDto : IParentableCreateDto<GroupSprint, Guid>
    {
        [Required]
        public Guid? GroupId { get; set; }
        public Guid? SprintId { get; set; }

        // Interface realization
        public void SetParentId(Guid parentId) => SprintId = parentId;
    }
}
