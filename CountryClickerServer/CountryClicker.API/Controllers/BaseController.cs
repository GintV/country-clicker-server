using CountryClicker.DataService;
using CountryClicker.DataService.Models.Create;
using CountryClicker.DataService.Models.Get;
using CountryClicker.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AutoMapper.Mapper;
using static CountryClicker.API.Constants;

namespace CountryClicker.API.Controllers
{
    public interface IBaseController<TEntity, TIdentifier, TCreateDto, TGetDto>
        where TEntity : IEntity
        where TIdentifier : IComparable
        where TCreateDto : ICreatableDto
        where TGetDto : IGetableDto<TIdentifier>
    {
        IActionResult CreateResource(TCreateDto createResource);
        IActionResult GetResource(TIdentifier id);
        IActionResult GetResources();
    }

    public class BaseController<TEntity, TIdentifier, TCreateDto, TGetDto> : Controller, IBaseController<TEntity, TIdentifier, TCreateDto, TGetDto>
        where TEntity : IEntity
        where TIdentifier : IComparable
        where TCreateDto : ICreatableDto
        where TGetDto : IGetableDto<TIdentifier>
    {
        protected IDataService<TEntity, TIdentifier> m_resourceDataService;

        public BaseController(IDataService<TEntity, TIdentifier> resourceDataService)
        {
            m_resourceDataService = resourceDataService;
        }

        [HttpGet(IdentifierPath)]
        public IActionResult GetResource(TIdentifier id)
        {
            var resource = m_resourceDataService.Get(id);

            if (resource == null)
                return NotFound();

            return Ok(Map<TGetDto>(resource));
        }

        [HttpGet]
        public IActionResult GetResources()
        {
            return Ok(Map<IEnumerable<TGetDto>>(m_resourceDataService.GetMany()));
        }

        [HttpPost]
        public virtual IActionResult CreateResource([FromBody] TCreateDto createResource)
        {
            if (createResource == null || !ModelState.IsValid)
                return BadRequest();

            var resource = Map<TEntity>(createResource);

            m_resourceDataService.Create(resource);
            m_resourceDataService.SaveChanges();

            var resourceToReturn = Map<TGetDto>(resource);

            return CreatedAtRoute(new { id = resourceToReturn.Id }, resourceToReturn);
        }
    }
}
