using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using static AutoMapper.Mapper;
using CountryClicker.API.Models.Update;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;

namespace CountryClicker.API.Controllers
{
    public class ContinentController : BaseController<Continent, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(Continent);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Continent);

        public ContinentController(IDataService<Continent, Guid> continentDataService) : base(continentDataService, m_getResourceRouteName) { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] ContinentCreateDto createDto) => 
            base.CreateResource<ContinentCreateDto, ContinentGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<ContinentGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<ContinentGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] ContinentUpdateDto updateDto) =>
            base.UpdateResource<ContinentUpdateDto, ContinentGetDto>(id, updateDto);
    }
}
