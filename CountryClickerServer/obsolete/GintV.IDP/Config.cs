using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace GintV.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "3D152423-8415-4F51-ACF3-2C0200526DBE",
                    Username = "TestA",
                    Password = "TestA",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "testa"),
                        new Claim("family_name", "testa"),
                        new Claim("role", "admin")
                    }
                },
                new TestUser
                {
                    SubjectId = "E049AD6F-3779-4CE4-8286-41AA2B9D2CD2",
                    Username = "TestB",
                    Password = "TestB",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "testb"),
                        new Claim("family_name", "testb"),
                        new Claim("role", "basic")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your role(s)", new [] { "role" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("countryclickerapi", "Country Clicker API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "test",
                    ClientName = "Test",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes =
                    {
                        "openid", "profile", "address", "roles", "countryclickerapi"
                    }
                }
            };
        }
    }
}
