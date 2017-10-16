using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Error;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Update;
using CountryClicker.API.RoutingParameters;
using CountryClicker.DataService;
using CountryClicker.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AutoMapper.Mapper;
using static CountryClicker.API.Constants;

namespace CountryClicker.API.Controllers
{
    public interface IBaseController<TEntity, TIdentifier>
        where TEntity : IEntity
    {
        IActionResult CreateResource<TCreateDto, TGetDto>(TCreateDto createDto)
            where TCreateDto : ICreateDto<TEntity>
            where TGetDto : IGetDto<TEntity, TIdentifier>;
        IActionResult CreateResource(TIdentifier id);
        IActionResult DeleteResource(TIdentifier id);
        IActionResult GetResource<TGetDto>(TIdentifier id)
            where TGetDto : IGetDto<TEntity, TIdentifier>;
        IActionResult GetResources<TGetDto>()
            where TGetDto : IGetDto<TEntity, TIdentifier>;
        IActionResult UpdateResource<TUpdateDto, TGetDto>(TIdentifier id, TUpdateDto updateDto)
            where TUpdateDto : IUpdateDto<TEntity>
            where TGetDto : IGetDto<TEntity, TIdentifier>;
    }

    public class BaseController<TEntity, TIdentifier> : Controller, IBaseController<TEntity, TIdentifier>
        where TEntity : class, IEntity
    {
        protected string GetResourceRouteName { get; }
        protected IGetResourceRouteParameters<TIdentifier> GetResourceRouteValues { get; }
        protected IDataService<TEntity, TIdentifier> ResourceDataService { get; }

        public BaseController(IDataService<TEntity, TIdentifier> resourceDataService, string getResourceRouteName,
            IGetResourceRouteParameters<TIdentifier> getResourceRouteValues = null)
        {
            ResourceDataService = resourceDataService;
            GetResourceRouteName = getResourceRouteName;
            GetResourceRouteValues = getResourceRouteValues;
        }

        public virtual IActionResult CreateResource<TCreateDto, TGetDto>(TCreateDto createDto)
            where TCreateDto : ICreateDto<TEntity>
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            if (createDto == null)
                return BadRequest(BadRequestDto.InvalidData);
            if (!ModelState.IsValid)
                return BadRequest(); // -- 400 missing required fields, consider mentioning which
            var resource = Map<TEntity>(createDto);
            if (!ResourceDataService.AreRelationshipsValid(resource))
                return NotFound(); // -- 404 parent with id does not exist, consider mentioning which
            ResourceDataService.Create(resource);
            ResourceDataService.SaveChanges();
            var resourceToReturn = Map<TGetDto>(resource);
            return CreatedAtRoute(GetResourceRouteName, GetResourceRouteValues?.GetRouteParameters(resourceToReturn.Id) ?? // consider changing so that parent create would be reflected correctly
                new { id = resourceToReturn.Id }, resourceToReturn);
        }

        public virtual IActionResult CreateResource(TIdentifier id) => ResourceDataService.Get(id) == null ?
            NotFound() : new StatusCodeResult(StatusCodes.Status409Conflict);

        public virtual IActionResult DeleteResource(TIdentifier id)
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null)
                return NotFound(); // -- 400 resource not found etc
            ResourceDataService.Delete(resource);
            ResourceDataService.SaveChanges();
            return NoContent();
        }

        public virtual IActionResult GetResource<TGetDto>(TIdentifier id)
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null)
                return NotFound();
            return Ok(Map<TGetDto>(resource));
        }

        public virtual IActionResult GetResources<TGetDto>()
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            return Ok(Map<IEnumerable<TGetDto>>(ResourceDataService.GetMany()));
        }

        public virtual IActionResult UpdateResource<TUpdateDto, TGetDto>(TIdentifier id, TUpdateDto updateDto)
            where TUpdateDto : IUpdateDto<TEntity>
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null)
                return NotFound(); // -- 404 no res 
            if (!ModelState.IsValid)
                return BadRequest(); // -- 400 missing req fields
            Map(updateDto, resource);
            if (!ResourceDataService.AreRelationshipsValid(resource))
                return NotFound(); // -- 404 parent with id does not exist, consider mentioning which
            ResourceDataService.Update(resource);
            ResourceDataService.SaveChanges();
            return Ok(Map<TGetDto>(resource));
        }
    }
}
