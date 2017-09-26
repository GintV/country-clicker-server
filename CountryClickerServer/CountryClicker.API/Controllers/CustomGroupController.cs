using System;
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

namespace CountryClicker.API.Controllers
{
    public class CustomGroupController : BaseParentableController<CustomGroup, Guid, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(CustomGroup);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Player) + PathSep + ParentId + PathSep + nameof(CustomGroup);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(CustomGroup);

        public CustomGroupController(IDataService<CustomGroup, Guid> customGroupDataService) :
            base(customGroupDataService, m_getResourceRouteName, nameof(Player))
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] CustomGroupCreateDto createDto) =>
            base.CreateResource<CustomGroupCreateDto, CustomGroupGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<CustomGroupGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<CustomGroupGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] CustomGroupUpdateDto updateDto) =>
            base.UpdateResource<CustomGroupUpdateDto, CustomGroupGetDto>(id, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] CustomGroupParentableCreateDto createDto) =>
            base.CreateParentableResource<CustomGroupParentableCreateDto, CustomGroupGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public override IActionResult CreateParentableResource(Guid parentId, Guid id) => base.CreateParentableResource(parentId, id);
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid id) => base.GetParentableResource<CustomGroupGetDto>(parentId, id);
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<CustomGroupGetDto>(parentId);
    }
}
