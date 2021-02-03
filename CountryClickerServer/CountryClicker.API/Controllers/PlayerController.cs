using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClicker.DataService;
using CountryClicker.Domain;
using AutoMapper;
using CountryClicker.API.Models.Update;
using static CountryClicker.API.Constants;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using CountryClicker.API.QueryingParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace CountryClicker.API.Controllers
{
    [Authorize]
    public class PlayerController : BaseController<Player, Guid>
    {
        private const string m_basePath = ApiBasePath + PathSep + nameof(Player);
        private const string m_basePathId = m_basePath + PathSep + Id;
        private const string m_getResourceRouteName = "Get" + nameof(Player);

        public PlayerController(IDataService<Player, Guid> playerDataService) : base(playerDataService,
            m_getResourceRouteName)
        {
        }

        [HttpPost(m_basePath)]
        public IActionResult CreateResource([FromBody] PlayerCreateDto createDto) =>
            base.CreateResource<PlayerCreateDto, PlayerGetDto>(createDto);

        [HttpPost(m_basePathId)]
        public new IActionResult CreateResource(Guid id) => base.CreateResource(id);

        [HttpDelete(m_basePathId)]
        public new IActionResult DeleteResource(Guid id) => base.DeleteResource(id);

        [HttpGet(m_basePathId, Name = m_getResourceRouteName)]
        public IActionResult GetResource(Guid id) => base.GetResource<PlayerGetDto>(id);

        [HttpGet(m_basePath), EnableCors("AllowMyClient")]
        public IActionResult GetResources(BaseResourceParameters baseResourceParameters, string userId) =>
            base.GetResources<PlayerGetDto>(baseResourceParameters, Filter(userId).ToArray());

        [HttpPut(m_basePathId)]
        public IActionResult UpdateResource(Guid id, [FromBody] PlayerUpdateDto updateDto) =>
            base.UpdateResource<PlayerUpdateDto, PlayerGetDto>(id, updateDto);

        private IEnumerable<(string column, string value)> Filter(string userId)
        {
            if (userId != null)
                yield return (nameof(userId), userId);
        }
    }
}