using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using static CountryClicker.API.Constants;
using AutoMapper;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.Models.Update;
using CountryClicker.API.QueryingParameters;
using Microsoft.AspNetCore.Authorization;

namespace CountryClicker.API.Controllers
{
    [Authorize]
    public class CityController : BaseChildController<City, Guid, Guid>
    {
        private const string BasePath = ApiBasePath + PathSep + nameof(City);
        private const string BasePathId = BasePath + PathSep + Id;
        private const string BasePathAsChild = ApiBasePath + PathSep + nameof(Country) + PathSep + ParentId + PathSep + nameof(City);
        private const string BasePathAsChildId = BasePathAsChild + PathSep + Id;
        private const string GetResourceRouteName = "Get" + nameof(City);

        public CityController(IDataService<City, Guid> cityDataService) : base(cityDataService, GetResourceRouteName, nameof(Country)) { }

        [HttpPost(BasePath)]
        public IActionResult CreateResource([FromBody] CityCreateDto createDto) => base.CreateResource<CityCreateDto, CityGetDto>(createDto);

        [HttpPost(BasePathId)]
        public new IActionResult CreateResource(Guid id) => base.CreateResource(id);

        [HttpDelete(BasePathId)]
        public new IActionResult DeleteResource(Guid id) => base.DeleteResource(id);

        [HttpGet(BasePathId, Name = GetResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<CityGetDto>(id);

        [HttpGet(BasePath)]
        public IActionResult GetResources(BaseResourceParameters baseResourceParameters) => base.GetResources<CityGetDto>(baseResourceParameters);

        [HttpPut(BasePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] CityUpdateDto updateDto) =>
            base.UpdateResource<CityUpdateDto, CityGetDto>(id, updateDto);

        [HttpPost(BasePathAsChild)]
        public IActionResult CreateResourceAsChild(Guid parentId, [FromBody] CityParentableCreateDto createDto) =>
            CreateResourceAsChild<CityParentableCreateDto, CityGetDto>(parentId, createDto);

        [HttpPost(BasePathAsChildId)]
        public new IActionResult CreateResourceAsChild(Guid parentId, Guid id) => base.CreateResourceAsChild(parentId, id);

        [HttpGet(BasePathAsChildId)]
        public IActionResult GetParentableResource(Guid parentId, Guid id) => base.GetResourceAsChild<CityGetDto>(parentId, id);

        [HttpGet(BasePathAsChild)]
        public IActionResult GetParentableResources(Guid parentId) => base.GetResourcesAsChildren<CityGetDto>(parentId);
    }
}
