using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using AutoMapper;
using CountryClicker.API.Models.Update;
using static CountryClicker.API.Constants;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;

namespace CountryClicker.API.Controllers
{
    public class PlayerController : BaseParentableController<Player, Guid, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(Player);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Domain.User) + PathSep + ParentId + PathSep + nameof(Player);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Player);

        public PlayerController(IDataService<Player, Guid> playerDataService) :
            base(playerDataService, m_getResourceRouteName, nameof(Domain.User))
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] PlayerCreateDto createDto) =>
            base.CreateResource<PlayerCreateDto, PlayerGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<PlayerGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<PlayerGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] PlayerUpdateDto updateDto) =>
            base.UpdateResource<PlayerUpdateDto, PlayerGetDto>(id, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] PlayerParentableCreateDto createDto) =>
            base.CreateParentableResource<PlayerParentableCreateDto, PlayerGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public override IActionResult CreateParentableResource(Guid parentId, Guid id) => base.CreateParentableResource(parentId, id);
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid id) => base.GetParentableResource<PlayerGetDto>(parentId, id);
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<PlayerGetDto>(parentId);
    }
}
