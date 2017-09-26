using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using CountryClicker.DataService.Models.Create;
using CountryClicker.DataService.Models.Get;
using static AutoMapper.Mapper;


namespace CountryClicker.API.Controllers
{
    public class SprintController : BaseController<Sprint, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(Sprint);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Sprint);

        public SprintController(IDataService<Sprint, Guid> sprintDataService) : base(sprintDataService, m_getResourceRouteName) { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] SprintCreateDto createDto) => base.CreateResource<SprintCreateDto, SprintGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<SprintGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<SprintGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] SprintUpdateDto updateDto) =>
            base.UpdateResource<SprintUpdateDto, SprintGetDto>(id, updateDto);
    }
}
