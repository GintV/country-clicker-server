using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Update;
using CountryClicker.API.RoutingParameters;
using CountryClicker.DataService;
using CountryClicker.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CountryClicker.API.QueryingParameters;
using static AutoMapper.Mapper;
using static CountryClicker.API.Models.Error.BadRequestDto;
using static CountryClicker.API.Models.Error.NotFoundDto;
using CountryClicker.API.Extensions;

namespace CountryClicker.API.Controllers
{
    public abstract class BaseController<TEntity, TIdentifier> : Controller
        where TEntity : class, IEntity
    {
        private readonly string _getResourceRouteName;
        private readonly IGetResourceRouteParameters<TIdentifier> _getResourceRouteValues;

        protected IDataService<TEntity, TIdentifier> ResourceDataService { get; }

        protected BaseController(IDataService<TEntity, TIdentifier> resourceDataService, string getResourceRouteName,
            IGetResourceRouteParameters<TIdentifier> getResourceRouteValues = null)
        {
            ResourceDataService = resourceDataService;
            _getResourceRouteName = getResourceRouteName;
            _getResourceRouteValues = getResourceRouteValues;
        }

        protected IActionResult CreateResource<TCreateDto, TGetDto>(TCreateDto createDto)
            where TCreateDto : ICreateDto<TEntity>
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            if (createDto == null)
                return BadRequest(InvalidData());
            if (!ModelState.IsValid)
                return BadRequest(); // TODO: -- 400 missing required fields, consider mentioning which
            var resource = Map<TEntity>(createDto);
            var result = ResourceDataService.AreRelationshipsValid(resource);
            if (!result.IsValid)
                return NotFound(ParentNotFound(result.NotFoundParentId));
            ResourceDataService.Create(resource);
            ResourceDataService.SaveChanges();
            var resourceToReturn = Map<TGetDto>(resource);
            return CreatedAtRoute(_getResourceRouteName, _getResourceRouteValues?.GetRouteParameters(resourceToReturn.Id) ?? // TODO: consider changing so that parent create would be reflected correctly
                new { id = resourceToReturn.Id }, resourceToReturn);
        }

        protected IActionResult CreateResource(TIdentifier id) => ResourceDataService.Get(id) == null ?
            NotFound() : new StatusCodeResult(StatusCodes.Status409Conflict);

        protected IActionResult DeleteResource(TIdentifier id)
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null)
                return NotFound(ResourceNotFound(id.ToString()));
            ResourceDataService.Delete(resource);
            ResourceDataService.SaveChanges();
            return NoContent();
        }

        protected IActionResult GetResource<TGetDto>(TIdentifier id)
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null)
                return NotFound(ResourceNotFound(id.ToString()));
            return Ok(Map<TGetDto>(resource));
        }

        protected IActionResult GetResources<TGetDto>(BaseResourceParameters baseResourceParameters,
            params (string column, string value)[] columnValuePairs) where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            return Ok(Map<IEnumerable<TGetDto>>(ResourceDataService.GetManyFilter(baseResourceParameters, columnValuePairs)));
        }

        protected IActionResult UpdateResource<TUpdateDto, TGetDto>(TIdentifier id, TUpdateDto updateDto)
            where TUpdateDto : IUpdateDto<TEntity>
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null)
                return NotFound(ResourceNotFound(id.ToString()));
            if (!ModelState.IsValid)
                return BadRequest(InvalidData());
            Map(updateDto, resource);
            var result = ResourceDataService.AreRelationshipsValid(resource);
            if (!result.IsValid)
                return NotFound(ParentNotFound(result.NotFoundParentId));
            ResourceDataService.Update(resource);
            ResourceDataService.SaveChanges();
            return Ok(Map<TGetDto>(resource));
        }
    }
}
