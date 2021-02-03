using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using AutoMapper;
using static CountryClicker.API.Constants;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Update;
using CountryClicker.API.Models.Get;
using CountryClicker.API.QueryingParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace CountryClicker.API.Controllers
{
    [Authorize]
    public class CustomGroupController : BaseChildController<CustomGroup, Guid, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(CustomGroup);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Player) + PathSep + ParentId + PathSep + nameof(CustomGroup);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(CustomGroup);

        public CustomGroupController(IDataService<CustomGroup, Guid> customGroupDataService) :
            base(customGroupDataService, m_getResourceRouteName, nameof(CustomGroup.CreatedBy))
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] CustomGroupCreateDto createDto) =>
            base.CreateResource<CustomGroupCreateDto, CustomGroupGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public new IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public new IActionResult DeleteResource(Guid id)
        {
            ResourceDataService.DeleteReferences(ResourceDataService.Get(id));
            return base.DeleteResource(id);
        }
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<CustomGroupGetDto>(id);
        [HttpGet(m_basePath), EnableCors("AllowMyClient")]
        public IActionResult GetResources(BaseResourceParameters baseResourceParameters) =>
            base.GetResources<CustomGroupGetDto>(baseResourceParameters);
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] CustomGroupUpdateDto updateDto) =>
            base.UpdateResource<CustomGroupUpdateDto, CustomGroupGetDto>(id, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateResourceAsChild(Guid parentId, [FromBody] CustomGroupParentableCreateDto createDto) =>
            base.CreateResourceAsChild<CustomGroupParentableCreateDto, CustomGroupGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public new IActionResult CreateResourceAsChild(Guid parentId, Guid id) => base.CreateResourceAsChild(parentId, id);
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetResourceAsChild(Guid parentId, Guid id) => base.GetResourceAsChild<CustomGroupGetDto>(parentId, id);
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetResourcesAsChildren(Guid parentId) => base.GetResourcesAsChildren<CustomGroupGetDto>(parentId);
    }
}
