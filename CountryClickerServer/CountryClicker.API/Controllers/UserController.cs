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
    public class UserController : BaseController<User, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(Domain.User);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Domain.User);


        public UserController(IDataService<User, Guid> userDataService) : base(userDataService, m_getResourceRouteName) { }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] UserCreateDto createDto) => base.CreateResource<UserCreateDto, UserGetDto>(createDto);
        [HttpPost(m_basePathId)]
        public override IActionResult CreateResource(Guid id) => base.CreateResource(id);
        [HttpDelete(m_basePathId)]
        public override IActionResult DeleteResource(Guid id) => base.DeleteResource(id);
        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<UserGetDto>(id);
        [HttpGet(m_basePath)]
        public IActionResult GetResources() => base.GetResources<UserGetDto>();
        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] UserUpdateDto updateDto) =>
            base.UpdateResource<UserUpdateDto, UserGetDto>(id, updateDto);
    }
}
