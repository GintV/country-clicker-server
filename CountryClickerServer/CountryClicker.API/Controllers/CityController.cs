using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using AutoMapper;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Update;

namespace CountryClicker.API.Controllers
{
    public class CityController : BaseParentableController<City, Guid, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(City);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_baseParentablePath = ApiBasePath + PathSep + nameof(Country) + PathSep + ParentId + PathSep + nameof(City);
        private const string m_baseParentablePathId = m_baseParentablePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(City);

        public CityController(IDataService<City, Guid> cityDataService) : base(cityDataService, m_getResourceRouteName, nameof(Country)) { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] CityCreateDto createDto) => base.CreateResource<CityCreateDto, CityGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<CityGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<CityGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] CityUpdateDto updateDto) =>
            base.UpdateResource<CityUpdateDto, CityGetDto>(id, updateDto);

        [HttpPost(m_baseParentablePath)]
        public IActionResult CreateParentableResource(Guid parentId, [FromBody] CityParentableCreateDto createDto) =>
            base.CreateParentableResource<CityParentableCreateDto, CityGetDto>(parentId, createDto);
        [HttpPost(m_baseParentablePathId)]
        public override IActionResult CreateParentableResource(Guid parentId, Guid id) => base.CreateParentableResource(parentId, id);
        [HttpGet(m_baseParentablePathId)]
        public IActionResult GetParentableResource(Guid parentId, Guid id) => base.GetParentableResource<CityGetDto>(parentId, id);
        [HttpGet(m_baseParentablePath)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetParentableResources<CityGetDto>(parentId);
    }
}
