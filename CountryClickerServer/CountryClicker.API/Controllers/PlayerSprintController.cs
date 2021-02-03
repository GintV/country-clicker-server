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
using CountryClicker.API.QueryingParameters;
using Microsoft.AspNetCore.Authorization;

namespace CountryClicker.API.Controllers
{
    [Authorize]
    public class PlayerSprintController : BaseChildController<PlayerSprint, Guid[], Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(PlayerSprint);
        private const string m_basePathId = m_basePath + PathSep + "({playerId},{sprintId})";
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Player) + PathSep + ParentId + PathSep + nameof(PlayerSprint);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + "({playerId},{sprintId})";
        private const string m_getResourceRouteName = "Get" + nameof(PlayerSprint);

        public PlayerSprintController(IDataService<PlayerSprint, Guid[]> playerSprintDataService) :
            base(playerSprintDataService, m_getResourceRouteName, nameof(Player), new PlayerSprintGetResourceRouteParameters())
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] PlayerSprintCreateDto createDto) =>
            base.CreateResource<PlayerSprintCreateDto, PlayerSprintGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public IActionResult CreateResource(Guid playerId, Guid sprintId) => base.CreateResource(new[] { playerId, sprintId });
        [HttpDelete(m_basePathId)]
        public IActionResult DeleteResource(Guid playerId, Guid sprintId) => base.DeleteResource(new[] { playerId, sprintId });
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid playerId, Guid sprintId) => base.GetResource<PlayerSprintGetDto>(new[] { playerId, sprintId });
        [HttpGet(m_basePath)]
        public IActionResult GetResources(BaseResourceParameters baseResourceParameters) =>
            base.GetResources<PlayerSprintGetDto>(baseResourceParameters);
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid playerId, Guid sprintId, [FromBody] PlayerSprintUpdateDto updateDto) =>
            base.UpdateResource<PlayerSprintUpdateDto, PlayerSprintGetDto>(new[] { playerId, sprintId }, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] PlayerSprintFromPlayerParentableCreateDto createDto) =>
            base.CreateResourceAsChild<PlayerSprintFromPlayerParentableCreateDto, PlayerSprintGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public IActionResult CreateParentableResource(Guid parentId, Guid playerId, Guid sprintId) =>
            base.CreateResourceAsChild(parentId, new[] { playerId, sprintId });
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid playerId, Guid sprintId) =>
            base.GetResourceAsChild<PlayerSprintGetDto>(parentId, new[] { playerId, sprintId });
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetResourcesAsChildren<PlayerSprintGetDto>(parentId);
    }

    public class PlayerSprint2Controller : BaseChildController<PlayerSprint, Guid[], Guid>
    {
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Sprint) + PathSep + ParentId + PathSep + nameof(PlayerSprint);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + "({playerId},{sprintId})";
        private const string m_getResourceRouteName = "Get" + nameof(PlayerSprint);

        public PlayerSprint2Controller(IDataService<PlayerSprint, Guid[]> playerSprintDataService) :
            base(playerSprintDataService, m_getResourceRouteName, nameof(Sprint), new PlayerSprintGetResourceRouteParameters())
        { }

        [HttpPost(m_baseParentablePath)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSprint) })]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] PlayerSprintFromSprintParentableCreateDto createDto) =>
            base.CreateResourceAsChild<PlayerSprintFromSprintParentableCreateDto, PlayerSprintGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSprint) })]
        public IActionResult CreateParentableResource(Guid parentId, Guid playerId, Guid sprintId) =>
            base.CreateResourceAsChild(parentId, new[] { playerId, sprintId });
        [HttpGet(m_baseParentablePathId)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSprint) })]
        public IActionResult GetParentableResource(Guid parentId, Guid playerId, Guid sprintId) =>
            base.GetResourceAsChild<PlayerSprintGetDto>(parentId, new[] { playerId, sprintId });
        [HttpGet(m_baseParentablePath)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSprint) })]
        public IActionResult GetParentableResources(Guid parentId) => base.GetResourcesAsChildren<PlayerSprintGetDto>(parentId);
    }
}
