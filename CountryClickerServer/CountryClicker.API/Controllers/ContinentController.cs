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
    [Route(ApiBasePath + nameof(Continent))]
    public class ContinentController : BaseController<Continent, Guid, ContinentCreateDto, ContinentGetDto>
    {
        public ContinentController(IDataService<Continent, Guid> continentDataService) : base(continentDataService) { }
    }
}
