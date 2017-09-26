﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using CountryClicker.DataService.Models.Create;
using AutoMapper;
using CountryClicker.DataService.Models.Get;
using static CountryClicker.API.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;
using CountryClicker.API.RoutingParameters;

namespace CountryClicker.API.Controllers
{
    public class PlayerSubscriptionController : BaseParentableController<PlayerSubscription, Guid[], Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(PlayerSubscription);
        private const string m_basePathId = m_basePath + PathSep + "({playerId},{groupId})";
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Player) + PathSep + ParentId + PathSep +
            nameof(PlayerSubscription);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + "({playerId},{groupId})";
        private const string m_getResourceRouteName = "Get" + nameof(PlayerSubscription);

        public PlayerSubscriptionController(IDataService<PlayerSubscription, Guid[]> playerSubscriptionDataService) :
            base(playerSubscriptionDataService, m_getResourceRouteName, nameof(Player), new PlayerSubscriptionGetResourceRouteParameters())
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] PlayerSubscriptionCreateDto createDto) =>
            base.CreateResource<PlayerSubscriptionCreateDto, PlayerSubscriptionGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public IActionResult CreateResource(Guid playerId, Guid groupId) => base.CreateResource(new[] { playerId, groupId });
        [HttpDelete(m_basePathId)]
        public IActionResult DeleteResource(Guid playerId, Guid groupId) => base.DeleteResource(new[] { playerId, groupId });
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid playerId, Guid groupId) => base.GetResource<PlayerSubscriptionGetDto>(new[] { playerId, groupId });
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<PlayerSubscriptionGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid playerId, Guid groupId, [FromBody] PlayerSubscriptionUpdateDto updateDto) =>
            base.UpdateResource<PlayerSubscriptionUpdateDto, PlayerSubscriptionGetDto>(new[] { playerId, groupId }, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] PlayerSubscriptionFromPlayerParentableCreateDto createDto) =>
            base.CreateParentableResource<PlayerSubscriptionFromPlayerParentableCreateDto, PlayerSubscriptionGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public IActionResult CreateParentableResource(Guid parentId, Guid playerId, Guid groupId) =>
            base.CreateParentableResource(parentId, new[] { playerId, groupId });
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid playerId, Guid groupId) =>
            base.GetParentableResource<PlayerSubscriptionGetDto>(parentId, new[] { playerId, groupId });
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<PlayerSubscriptionGetDto>(parentId);
    }

    public class PlayerSubscription2Controller : BaseParentableController<PlayerSubscription, Guid[], Guid>
    {
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Group) + PathSep + ParentId + PathSep +
            nameof(PlayerSubscription);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + "({playerId},{groupId})";
        private const string m_getResourceRouteName = "Get" + nameof(PlayerSubscription);

        public PlayerSubscription2Controller(IDataService<PlayerSubscription, Guid[]> playerSubscriptionDataService) :
            base(playerSubscriptionDataService, m_getResourceRouteName, nameof(Group), new PlayerSubscriptionGetResourceRouteParameters())
        { }

        [HttpPost(m_baseParentablePath)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSubscription) })]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] PlayerSubscriptionFromGroupParentableCreateDto createDto) =>
            base.CreateParentableResource<PlayerSubscriptionFromGroupParentableCreateDto, PlayerSubscriptionGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSubscription) })]
        public IActionResult CreateParentableResource(Guid parentId, Guid playerId, Guid groupId) =>
            base.CreateParentableResource(parentId, new[] { playerId, groupId });
        [HttpGet(m_baseParentablePathId)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSubscription) })]
        public IActionResult GetParentableResource(Guid parentId, Guid playerId, Guid groupId) =>
            base.GetParentableResource<PlayerSubscriptionGetDto>(parentId, new[] { playerId, groupId });
        [HttpGet(m_baseParentablePath)]
        [SwaggerOperation(Tags = new[] { nameof(PlayerSubscription) })]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<PlayerSubscriptionGetDto>(parentId);
    }
}
