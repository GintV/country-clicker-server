using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CountryClicker.Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CountryClicker.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICountryClickerHttpClient m_countryClickerHttpClient;

        public HomeController(ICountryClickerHttpClient countryClickerHttpClient)
        {
            m_countryClickerHttpClient = countryClickerHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();
            return Content(await m_countryClickerHttpClient.GetValidAccessToken());
        }

        public async Task Logout()
        {
            // get the metadata
            var discoveryClient = new DiscoveryClient("https://localhost:44373/");
            var metaDataResponse = await discoveryClient.GetAsync();

            // get revocation client
            var revocationClient = new TokenRevocationClient(metaDataResponse.RevocationEndpoint, "countryclickerclient", "ItsMySecret");

            await RevokeAccessToken(revocationClient);
            await RevokeRefreshToken(revocationClient);

            // sign-out of authentication schemes
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        private async Task RevokeAccessToken(TokenRevocationClient revocationClient)
        {
            // get access token
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                // revoke access token
                var revokeAccessTokenResponse = await revocationClient.RevokeAccessTokenAsync(accessToken);
                if (revokeAccessTokenResponse.IsError)
                {
                    throw new Exception("Error occurred during revocation of access token", revokeAccessTokenResponse.Exception);
                }
            }
        }

        private async Task RevokeRefreshToken(TokenRevocationClient revocationClient)
        {
            // get refresh token
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                // revoke refresh token
                var revokeRefreshTokenResponse = await revocationClient.RevokeRefreshTokenAsync(refreshToken);
                if (revokeRefreshTokenResponse.IsError)
                {
                    throw new Exception("Error occurred during revocation of refresh token", revokeRefreshTokenResponse.Exception);
                }
            }
        }

        public async Task WriteOutIdentityInformation()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            Debug.WriteLine($"IdentityToken: {identityToken}");
            foreach(var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type}, claim value: {claim.Value}");
            }
        }
    }
}
