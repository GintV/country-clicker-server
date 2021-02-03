using System;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using CountryClicker.API.Models.Update;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Create;
using CountryClicker.API.QueryingParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace CountryClicker.API.Controllers
{
    [Authorize]
    public class CountryController : BaseChildController<Country, Guid, Guid>
    {
        private const string BasePath = ApiBasePath + PathSep + nameof(Country);
        private const string BasePathId = BasePath + PathSep + Id;
        private const string BaseParentablePath = ApiBasePath + PathSep + nameof(Continent) + PathSep + ParentId + PathSep + nameof(Country);
        private const string m_baseParentablePathId = BaseParentablePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Country);

        public CountryController(IDataService<Country, Guid> countryDataService) :
            base(countryDataService, m_getResourceRouteName, nameof(Continent))
        { }

        [HttpPost(BasePath)]
        public IActionResult CreateResource([FromBody] CountryCreateDto createDto) =>
            base.CreateResource<CountryCreateDto, CountryGetDto>(createDto);
        [HttpPost(BasePathId)]
        public new IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(BasePathId)]
        public new IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(BasePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => GetResource<CountryGetDto>(id);
        [HttpGet(BasePath), EnableCors("AllowMyClient")]
        public IActionResult GetResources(BaseResourceParameters baseResourceParameters) => GetResources<CountryGetDto>(baseResourceParameters);
        [HttpPut(BasePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] CountryUpdateDto updateDto) =>
            base.UpdateResource<CountryUpdateDto, CountryGetDto>(id, updateDto);

        [HttpPost(BaseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] CountryParentableCreateDto createDto) =>
            base.CreateResourceAsChild<CountryParentableCreateDto, CountryGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public new IActionResult CreateResourceAsChild(Guid parentId, Guid id) => base.CreateResourceAsChild(parentId, id);
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid id) => GetResourceAsChild<CountryGetDto>(parentId, id);
        [HttpGet(BaseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => GetResourcesAsChildren<CountryGetDto>(parentId);
    }
}
