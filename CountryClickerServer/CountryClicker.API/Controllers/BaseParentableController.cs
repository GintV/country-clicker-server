using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
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

namespace CountryClicker.API.Controllers
{
    public interface IBaseParentableController<TEntity, TIdentifier, TParentIdentifier> : IBaseController<TEntity, TIdentifier>
        where TEntity : IParentableEntity<TParentIdentifier>
    {
        IActionResult CreateParentableResource<TCreateDto, TGetDto>(TParentIdentifier parentId, TCreateDto createDto)
            where TCreateDto : IParentableCreateDto<TEntity, TParentIdentifier>
            where TGetDto : IGetDto<TEntity, TIdentifier>;
        IActionResult GetParentableResource<TGetDto>(TParentIdentifier parentId, TIdentifier id)
            where TGetDto : IGetDto<TEntity, TIdentifier>;
        IActionResult GetParentableResources<TGetDto>(TParentIdentifier parentId)
            where TGetDto : IGetDto<TEntity, TIdentifier>;
    }

    public class BaseParentableController<TEntity, TIdentifier, TParentIdentifier> : BaseController<TEntity, TIdentifier>,
        IBaseParentableController<TEntity, TIdentifier, TParentIdentifier>
        where TEntity : class, IParentableEntity<TParentIdentifier>
    {
        protected string ParentEntityName { get; }

        public BaseParentableController(IDataService<TEntity, TIdentifier> resourceDataService, string getResourceRouteName, string parentEntityName,
            IGetResourceRouteParameters<TIdentifier> getResourceRouteValues = null) :
            base(resourceDataService, getResourceRouteName, getResourceRouteValues)
        {
            ParentEntityName = parentEntityName;
        }

        public virtual IActionResult CreateParentableResource<TCreateDto, TGetDto>(TParentIdentifier parentId, TCreateDto createDto)
           where TCreateDto : IParentableCreateDto<TEntity, TParentIdentifier>
           where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            createDto?.SetParentId(parentId);
            return base.CreateResource<TCreateDto, TGetDto>(createDto);
        }

        public virtual IActionResult CreateParentableResource(TParentIdentifier parentId, TIdentifier id)
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null || !resource.ParentId(ParentEntityName).Equals(parentId))
                return NotFound();
            return new StatusCodeResult(StatusCodes.Status409Conflict);
        }

        public virtual IActionResult GetParentableResource<TGetDto>(TParentIdentifier parentId, TIdentifier id)
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var resource = ResourceDataService.Get(id);
            if (resource == null || !resource.ParentId(ParentEntityName).Equals(parentId))
                return NotFound();
            return Ok(Map<TGetDto>(resource));
        }

        public virtual IActionResult GetParentableResources<TGetDto>(TParentIdentifier parentId)
            where TGetDto : IGetDto<TEntity, TIdentifier>
        {
            var result = ResourceDataService.GetManyFilter(($"{ParentEntityName}Id", parentId.ToString()));
            return Ok(Map<IEnumerable<TGetDto>>(result));
        }
    }
}
