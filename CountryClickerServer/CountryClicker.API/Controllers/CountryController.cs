using System;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using CountryClicker.API.Models.Update;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Create;

namespace CountryClicker.API.Controllers
{
    public class CountryController : BaseParentableController<Country, Guid, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(Country);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Continent) + PathSep + ParentId + PathSep + nameof(Country);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Country);

        public CountryController(IDataService<Country, Guid> countryDataService) :
            base(countryDataService, m_getResourceRouteName, nameof(Continent))
        { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] CountryCreateDto createDto) =>
            base.CreateResource<CountryCreateDto, CountryGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<CountryGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<CountryGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] CountryUpdateDto updateDto) =>
            base.UpdateResource<CountryUpdateDto, CountryGetDto>(id, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] CountryParentableCreateDto createDto) =>
            base.CreateParentableResource<CountryParentableCreateDto, CountryGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public override IActionResult CreateParentableResource(Guid parentId, Guid id) => base.CreateParentableResource(parentId, id);
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid id) => base.GetParentableResource<CountryGetDto>(parentId, id);
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<CountryGetDto>(parentId);
    }
}
