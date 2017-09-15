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
    [Route(ApiBasePath + nameof(Continent))]
    public class ContinentController : Controller
    {
        private IDataService<Continent, Guid> m_continentDataService;

        public ContinentController(IDataService<Continent, Guid> continentDataService)
        {
            m_continentDataService = continentDataService;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var result = m_continentDataService.GetMany();
            return new JsonResult(result);
        }

        [HttpGet("{id}", Name ="GetContinent")]
        public IActionResult GetContinent(Guid id)
        {
            var result = m_continentDataService.Get(id);

            if (result == null)
                return NotFound();

            var instance = Mapper.Map<ContinentGetDto>(result);
            return Ok(instance);
        }

        [HttpPost]
        public IActionResult CreateContinent([FromBody] ContinentCreateDto continent)
        {
            if (continent == null)
                return BadRequest();

            var instance = Mapper.Map<Continent>(continent);

            m_continentDataService.Create(instance);
            m_continentDataService.SaveChanges();

            var continentr = Mapper.Map<ContinentGetDto>(instance);

            return CreatedAtRoute("GetContinent", new { id = continentr.Id }, continentr);
        }
    }
}
