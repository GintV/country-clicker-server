using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using CountryClicker.DataService.Models.Create;
using AutoMapper;
using CountryClicker.DataService.Models.Get;

namespace CountryClicker.API.Controllers
{
    [Route(ApiBasePath + nameof(Country))]
    public class CountryController : BaseController<Country, Guid, CountryCreateDto, CountryGetDto>
    {
        public CountryController(IDataService<Country, Guid> countryDataService) : base(countryDataService) { }

        [HttpPost]
        public override IActionResult CreateResource([FromBody] CountryCreateDto createResource)
        {
            if (createResource != null && createResource.ContinentId.HasValue && m_resourceDataService.Exists(createResource.ContinentId.Value))
                return base.CreateResource(createResource);

            return BadRequest();
        }
    }
}
