using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CountryClicker.Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using CountryClicker.API.Models.Create;
using CountryClicker.API.Models.Get;
using IdentityServer4.Extensions;
using Newtonsoft.Json;

namespace CountryClicker.Client.Controllers
{
    [Authorize]
    public class DarkController : Controller
    {
        private const string SessionPlayerId = "playerId";
        private const string SessionSubsCount = "subsCount";
        private const string SessionOwnedGroupsCount = "ownedGroupsCount";

        private readonly ICountryClickerHttpClient m_countryClickerHttpClient;

        public DarkController(ICountryClickerHttpClient countryClickerHttpClient)
        {
            m_countryClickerHttpClient = countryClickerHttpClient;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Click");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Click()
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Click(ClicksPostModel model)
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");

            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            if (playerId == null)
                return RedirectToAction("Logout", "Home");

            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.GetAsync($"Api/Player/{playerId}/PlayerSubscription").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return RedirectToAction("Logout", "Home");

            var groupIds = JsonConvert.DeserializeObject<IList<PlayerSubscriptionGetDto>>(responseString).Select(sub => sub.GroupId);

            foreach (var groupId in groupIds)
            {
                response = await httpClient.GetAsync($"Api/CustomGroup/{groupId}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var customGroup = JsonConvert.DeserializeObject<CustomGroupGetDto>(responseString);
                    customGroup.Score += model.Count;
                    response = await httpClient.PutAsync($"Api/CustomGroup/{groupId}", new StringContent(JsonConvert.SerializeObject(customGroup), Encoding.UTF8,
                        "application/json"));
                    continue;
                }

                response = await httpClient.GetAsync($"Api/Continent/{groupId}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var continent = JsonConvert.DeserializeObject<ContinentGetDto>(responseString);
                    continent.Score += model.Count;
                    response = await httpClient.PutAsync($"Api/Continent/{groupId}", new StringContent(JsonConvert.SerializeObject(continent), Encoding.UTF8,
                        "application/json"));
                    continue;
                }

                response = await httpClient.GetAsync($"Api/Country/{groupId}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var country = JsonConvert.DeserializeObject<CountryGetDto>(responseString);
                    country.Score += model.Count;
                    response = await httpClient.PutAsync($"Api/Country/{groupId}", new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8,
                        "application/json"));
                    continue;
                }

                response = await httpClient.GetAsync($"Api/City/{groupId}").ConfigureAwait(false);
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var city = JsonConvert.DeserializeObject<CountryGetDto>(responseString);
                city.Score += model.Count;
                response = await httpClient.PutAsync($"Api/City/{groupId}", new StringContent(JsonConvert.SerializeObject(city), Encoding.UTF8,
                    "application/json"));
            }

            response = await httpClient.GetAsync($"Api/Player/{playerId}").ConfigureAwait(false);
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var player = JsonConvert.DeserializeObject<PlayerGetDto>(responseString);
            player.Score += model.Count;
            await httpClient.PutAsync($"Api/Player/{playerId}", new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8,
                "application/json"));

            return RedirectToAction("ClickRedirect");
        }

        [HttpGet]
        public IActionResult ClickRedirect() => RedirectToAction("Click");

        [HttpGet]
        public async Task<IActionResult> CreateGroup()
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");

            var ownedGroupsCount = HttpContext.Session.GetInt32(SessionOwnedGroupsCount);
            if (ownedGroupsCount == null)
                return RedirectToAction("Logout", "Home");

            return View(new CreateGroupModel { CanCreate = ownedGroupsCount.Value < 5 });
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupModel model)
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");

            var ownedGroupsCount = HttpContext.Session.GetInt32(SessionOwnedGroupsCount);
            if (ownedGroupsCount == null)
                return RedirectToAction("Logout", "Home");
            if (ownedGroupsCount.Value > 4)
                return View(new CreateGroupModel { Title = model.Title, CanCreate = false });

            if (!await CreateAndSubscribeToGroup(model.Title))
                return RedirectToAction("Logout", "Home");

            return RedirectToAction("OwnedGroups");
        }

        [HttpGet]
        public async Task<IActionResult> OwnedGroups()
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");

            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            if (playerId == null)
                return RedirectToAction("Logout", "Home");

            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.GetAsync($"Api/Player/{playerId}/CustomGroup").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return RedirectToAction("Logout", "Home");
            var ownedGroups = JsonConvert.DeserializeObject<IList<CustomGroupGetDto>>(responseString);

            return View(ownedGroups);
        }

        [HttpGet]
        public async Task<IActionResult> Subscriptions()
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");

            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            if (playerId == null)
                return RedirectToAction("Logout", "Home");

            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.GetAsync($"Api/Player/{playerId}/PlayerSubscription").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return RedirectToAction("Logout", "Home");

            var groupIds = JsonConvert.DeserializeObject<IList<PlayerSubscriptionGetDto>>(responseString).Select(sub => sub.GroupId);

            var model = new SubscriptionsModel();

            var subsCount = HttpContext.Session.GetInt32(SessionSubsCount);
            if (subsCount != null)
                model.CanSubscribe = subsCount.Value < 10;

            foreach (var groupId in groupIds)
            {
                response = await httpClient.GetAsync($"Api/CustomGroup/{groupId}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var customGroup = JsonConvert.DeserializeObject<CustomGroupGetDto>(responseString);
                    model.CustomGroups.Add(customGroup);
                    continue;
                }

                response = await httpClient.GetAsync($"Api/Continent/{groupId}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var continent = JsonConvert.DeserializeObject<ContinentGetDto>(responseString);
                    model.Continent = continent;
                    continue;
                }

                response = await httpClient.GetAsync($"Api/Country/{groupId}").ConfigureAwait(false);
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var country = JsonConvert.DeserializeObject<CountryGetDto>(responseString);
                model.Country = country;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Subscribe()
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");

            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            if (playerId == null)
                return RedirectToAction("Logout", "Home");

            var subsCount = HttpContext.Session.GetInt32(SessionSubsCount);
            if (subsCount != null && subsCount.Value > 9)
                return RedirectToAction("Subscriptions");

            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.GetAsync($"Api/Player/{playerId}/PlayerSubscription").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return RedirectToAction("Logout", "Home");

            var groupIds = JsonConvert.DeserializeObject<IList<PlayerSubscriptionGetDto>>(responseString).Select(sub => sub.GroupId);

            response = await httpClient.GetAsync($"Api/CustomGroup").ConfigureAwait(false);
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var customGroups = JsonConvert.DeserializeObject<IList<CustomGroupGetDto>>(responseString).Where(group => !groupIds.Contains(group.Id)).
                ToList();

            var model = new SubscriptionsModel { CustomGroups = customGroups };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            if (HttpContext.Session.GetInt32(SessionOwnedGroupsCount) == null && !await CheckInPlayer())
                return RedirectToAction("Logout", "Home");
            return View();
        }


        public async Task<IActionResult> DeleteGroup(string id)
        {
            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.DeleteAsync($"Api/CustomGroup/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var subsCount = HttpContext.Session.GetInt32(SessionSubsCount);
                if (subsCount != null)
                    HttpContext.Session.SetInt32(SessionSubsCount, subsCount.Value - 1);
                var ownedGroupsCount = HttpContext.Session.GetInt32(SessionOwnedGroupsCount);
                if (ownedGroupsCount != null)
                    HttpContext.Session.SetInt32(SessionOwnedGroupsCount, ownedGroupsCount.Value - 1);
            }

            return RedirectToAction("OwnedGroups");
        }

        public async Task<IActionResult> UnsubscribeGroup(string id)
        {
            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            if (playerId == null)
                return RedirectToAction("Logout", "Home");

            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.DeleteAsync($"Api/PlayerSubscription/({playerId},{id})").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var subsCount = HttpContext.Session.GetInt32(SessionSubsCount);
                if (subsCount != null)
                    HttpContext.Session.SetInt32(SessionSubsCount, subsCount.Value - 1);
            }

            return RedirectToAction("Subscriptions");
        }

        public async Task<IActionResult> SubscribeGroup(string id)
        {
            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            if (playerId == null)
                return RedirectToAction("Logout", "Home");

            var subsCount = HttpContext.Session.GetInt32(SessionSubsCount);
            if (subsCount != null && subsCount.Value > 9)
                return RedirectToAction("Subscriptions");

            var httpClient = await m_countryClickerHttpClient.GetClient();
            var sub = new PlayerSubscriptionCreateDto { GroupId = Guid.Parse(id), PlayerId = Guid.Parse(playerId) };
            var response = await httpClient.PostAsync($"Api/PlayerSubscription", new StringContent(JsonConvert.SerializeObject(sub), Encoding.UTF8,
                "application/json"));
            if (response.IsSuccessStatusCode)
                HttpContext.Session.SetInt32(SessionSubsCount, subsCount.Value + 1);

            return RedirectToAction("Subscriptions");
        }

        private async Task<bool> CheckInPlayer()
        {
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            if (userId == null)
                throw new Exception("A problem occurred getting user id.");
            var httpClient = await m_countryClickerHttpClient.GetClient();
            var response = await httpClient.GetAsync($"Api/Player?userId={userId}").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return false;
            var playerId = JsonConvert.DeserializeObject<IList<PlayerGetDto>>(responseString).FirstOrDefault()?.Id;
            if (playerId == null)
            {
                var nickname = User.Claims.FirstOrDefault(claim => claim.Type == "nickname")?.Value;
                if (nickname == null)
                    throw new Exception("A problem occurred getting user nickname.");
                var player = new PlayerCreateDto { Nickname = nickname, UserId = userId };
                response = await httpClient.PostAsync("Api/Player", new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8,
                    "application/json"));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception("A problem occurred creating player.");
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                playerId = JsonConvert.DeserializeObject<PlayerGetDto>(responseString).Id;

                var country = User.Claims.FirstOrDefault(claim => claim.Type == "country")?.Value;
                if (country == null)
                    throw new Exception("A problem occurred getting user country.");
                var countrySub = new PlayerSubscriptionCreateDto { GroupId = Guid.Parse(country), PlayerId = playerId };
                response = await httpClient.PostAsync("Api/PlayerSubscription", new StringContent(JsonConvert.SerializeObject(countrySub), Encoding.UTF8,
                    "application/json"));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception("A problem occurred while subscribing to country.");

                response = await httpClient.GetAsync($"Api/Country/{country}").ConfigureAwait(false);
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (responseString.IsNullOrEmpty())
                    return false;
                var continentId = JsonConvert.DeserializeObject<CountryGetDto>(responseString).ContinentId;
                var continentSub = new PlayerSubscriptionCreateDto { GroupId = continentId, PlayerId = playerId };
                response = await httpClient.PostAsync("Api/PlayerSubscription", new StringContent(JsonConvert.SerializeObject(continentSub), Encoding.UTF8,
                    "application/json"));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception("A problem occurred while subscribing to continent.");

            }
            HttpContext.Session.SetString(SessionPlayerId, playerId.ToString());

            response = await httpClient.GetAsync($"Api/Player/{playerId}/PlayerSubscription").ConfigureAwait(false);
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return false;
            var subsCount = JsonConvert.DeserializeObject<IList<PlayerSubscriptionGetDto>>(responseString).Count;
            HttpContext.Session.SetInt32(SessionSubsCount, subsCount);

            response = await httpClient.GetAsync($"Api/Player/{playerId}/CustomGroup").ConfigureAwait(false);
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return false;
            var ownedGroupsCount = JsonConvert.DeserializeObject<IList<CustomGroupGetDto>>(responseString).Count;
            HttpContext.Session.SetInt32(SessionOwnedGroupsCount, ownedGroupsCount);

            return true;
        }

        private async Task<bool> CreateAndSubscribeToGroup(string groupTitle)
        {
            var httpClient = await m_countryClickerHttpClient.GetClient();
            var playerId = HttpContext.Session.GetString(SessionPlayerId);
            var customGroup = new CustomGroupCreateDto { Title = groupTitle };
            var response = await httpClient.PostAsync($"Api/Player/{playerId}/CustomGroup", new StringContent(JsonConvert.SerializeObject(customGroup), Encoding.UTF8,
                "application/json")).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return false;

            var ownedGroupsCount = HttpContext.Session.GetInt32(SessionOwnedGroupsCount);
            if (ownedGroupsCount == null)
                return false;
            HttpContext.Session.SetInt32(SessionOwnedGroupsCount, ownedGroupsCount.Value + 1);
            var groupId = JsonConvert.DeserializeObject<CustomGroupGetDto>(responseString).Id;

            var sub = new PlayerSubscriptionCreateDto { GroupId = groupId };
            response = await httpClient.PostAsync($"Api/Player/{playerId}/PlayerSubscription", new StringContent(JsonConvert.SerializeObject(sub), Encoding.UTF8,
                "application/json")).ConfigureAwait(false);
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseString.IsNullOrEmpty())
                return false;

            var subsCount = HttpContext.Session.GetInt32(SessionSubsCount);
            if (subsCount == null)
                return false;
            HttpContext.Session.SetInt32(SessionSubsCount, subsCount.Value + 1);

            return true;
        }


    }

    public class ClicksPostModel
    {
        [Required]
        public int Count { get; set; }
    }

    public class CreateGroupModel
    {
        [MaxLength(50), Required]
        public string Title { get; set; }

        public bool CanCreate { get; set; } = true;
    }

    public class SubscriptionsModel
    {
        public ContinentGetDto Continent { get; set; }

        public CountryGetDto Country { get; set; }

        public CityGetDto City { get; set; }

        public IList<CustomGroupGetDto> CustomGroups { get; set; } = new List<CustomGroupGetDto>();

        public bool CanSubscribe { get; set; }
    }
}
