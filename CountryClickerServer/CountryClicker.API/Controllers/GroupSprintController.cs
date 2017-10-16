using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using AutoMapper;
using static CountryClicker.API.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;
using CountryClicker.API.RoutingParameters;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Update;

namespace CountryClicker.API.Controllers
{
    public class GroupSprintController : BaseParentableController<GroupSprint, Guid[], Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(GroupSprint);
        private const string m_basePathId = m_basePath + PathSep + "({groupId},{sprintId})";
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Group) + PathSep + ParentId + PathSep + nameof(GroupSprint);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + "({groupId},{sprintId})";
        private const string m_getResourceRouteName = "Get" + nameof(GroupSprint);

        public GroupSprintController(IDataService<GroupSprint, Guid[]> groupSprintDataService) :
            base(groupSprintDataService, m_getResourceRouteName, nameof(Group), new GroupSprintGetResourceRouteParameters())
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] GroupSprintCreateDto createDto) =>
            base.CreateResource<GroupSprintCreateDto, GroupSprintGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public IActionResult CreateResource(Guid groupId, Guid sprintId) => base.CreateResource(new[] { groupId, sprintId });
        [HttpDelete(m_basePathId)]
        public IActionResult DeleteResource(Guid groupId, Guid sprintId) => base.DeleteResource(new[] { groupId, sprintId });
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid groupId, Guid sprintId) => base.GetResource<GroupSprintGetDto>(new[] { groupId, sprintId });
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<GroupSprintGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid groupId, Guid sprintId, [FromBody] GroupSprintUpdateDto updateDto) =>
            base.UpdateResource<GroupSprintUpdateDto, GroupSprintGetDto>(new[] { groupId, sprintId }, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] GroupSprintFromGroupParentableCreateDto createDto) =>
            base.CreateParentableResource<GroupSprintFromGroupParentableCreateDto, GroupSprintGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public IActionResult CreateParentableResource(Guid parentId, Guid groupId, Guid sprintId) =>
            base.CreateParentableResource(parentId, new[] { groupId, sprintId });
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid groupId, Guid sprintId) =>
            base.GetParentableResource<GroupSprintGetDto>(parentId, new[] { groupId, sprintId });
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<GroupSprintGetDto>(parentId);
    }

    public class GroupSprint2Controller : BaseParentableController<GroupSprint, Guid[], Guid>
    {
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Sprint) + PathSep + ParentId + PathSep + nameof(GroupSprint);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + "({groupId},{sprintId})";
        private const string m_getResourceRouteName = "Get" + nameof(GroupSprint);

        public GroupSprint2Controller(IDataService<GroupSprint, Guid[]> groupSprintDataService) :
            base(groupSprintDataService, m_getResourceRouteName, nameof(Sprint), new GroupSprintGetResourceRouteParameters())
        { }

        [HttpPost(m_baseParentablePath)]
        [SwaggerOperation(Tags = new[] { nameof(GroupSprint) })]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] GroupSprintFromSprintParentableCreateDto createDto) =>
            base.CreateParentableResource<GroupSprintFromSprintParentableCreateDto, GroupSprintGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        [SwaggerOperation(Tags = new[] { nameof(GroupSprint) })]
        public IActionResult CreateParentableResource(Guid parentId, Guid groupId, Guid sprintId) =>
            base.CreateParentableResource(parentId, new[] { groupId, sprintId });
        [HttpGet(m_baseParentablePathId)]
        [SwaggerOperation(Tags = new[] { nameof(GroupSprint) })]
        public IActionResult GetParentableResource(Guid parentId, Guid groupId, Guid sprintId) =>
            base.GetParentableResource<GroupSprintGetDto>(parentId, new[] { groupId, sprintId });
        [HttpGet(m_baseParentablePath)]
        [SwaggerOperation(Tags = new[] { nameof(GroupSprint) })]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<GroupSprintGetDto>(parentId);
    }
}
