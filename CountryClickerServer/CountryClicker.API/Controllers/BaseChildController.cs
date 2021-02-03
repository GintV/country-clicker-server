using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.RoutingParameters;
using CountryClicker.DataService;
using CountryClicker.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static AutoMapper.Mapper;

namespace CountryClicker.API.Controllers
{
    public abstract class BaseChildController<TEntity, TIdentifier, TParentIdentifier> : BaseController<TEntity, TIdentifier>
        where TEntity : class, IParentableEntity<TParentIdentifier>
    {
        private readonly string _parentEntityName;

        protected BaseChildController(IDataService<TEntity, TIdentifier> resourceDataService, string getResourceRouteName, string parentEntityName,
            IGetResourceRouteParameters<TIdentifier> getResourceRouteValues = null) :
            base(resourceDataService, getResourceRouteName, getResourceRouteValues)
        {
            _parentEntityName = parentEntityName;
        }

        protected IActionResult CreateResourceAsChild<TCreateDto, TGetDto>(TParentIdentifier parentId, TCreateDto createDto)
           where TCreateDto : IParentableCreateDto<TEntity, TParentIdentifier>
           where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            createDto?.SetParentId(parentId);
            return CreateResource<TCreateDto, TGetDto>(createDto);
        }

        protected IActionResult CreateResourceAsChild(TParentIdentifier parentId, TIdentifier id)
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null || !resource.ParentId(_parentEntityName).Equals(parentId))
                return NotFound();
            return new StatusCodeResult(StatusCodes.Status409Conflict);
        }

        protected IActionResult GetResourceAsChild<TGetDto>(TParentIdentifier parentId, TIdentifier id)
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null || !resource.ParentId(_parentEntityName).Equals(parentId))
                return NotFound();
            return Ok(Map<TGetDto>(resource));
        }

        protected IActionResult GetResourcesAsChildren<TGetDto>(TParentIdentifier parentId)
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var result = ResourceDataService.GetManyFilter(($"{_parentEntityName}Id", parentId.ToString()));
            return Ok(Map<IEnumerable<TGetDto>>(result));
        }
    }
}
