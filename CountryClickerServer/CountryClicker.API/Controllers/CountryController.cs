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
    public class CountryController : Controller
    {
        private IDataService<Country, Guid> m_countryDataService;

        public CountryController(IDataService<Country, Guid> countryDataService)
        {
            m_countryDataService = countryDataService;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var result = m_countryDataService.GetMany();
            return new JsonResult(result);
        }

        [HttpGet("{id}", Name ="GetCountry")]
        public IActionResult GetCountry(Guid id)
        {
            var result = m_countryDataService.Get(id);

            if (result == null)
                return NotFound();

            var instance = Mapper.Map<CountryGetDto>(result);
            return Ok(instance);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryCreateDto country)
        {
            if (country == null)
                return BadRequest();

            var instance = Mapper.Map<Country>(country);

            m_countryDataService.Create(instance);
            m_countryDataService.SaveChanges();

            var countryr = Mapper.Map<CountryGetDto>(instance);


            return CreatedAtRoute("GetCountry", new { id = countryr.Id }, countryr);
        }
    }
}
