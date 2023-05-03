using IdentityModel;
using IdentityServer4;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;

namespace Shop.OAuth.Setup;

public class IdentityServerConfiguration
{
    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser()
            {
                SubjectId = "222", Username = "Fernando Andrade", Password = "11122as33",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Fernando Andrade"),
                    new Claim(JwtClaimTypes.GivenName, "Fernando"),
                    new Claim(JwtClaimTypes.FamilyName, "Andrade"),
                    new Claim(JwtClaimTypes.WebSite, "https://macoratti.net"),
                }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("myApi.read"),
            new ApiScope("myApi.write"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("myApi")
            {
                Scopes = new List<string> { "myApi.read", "myApi.write" },
                ApiSecrets = new List<Secret> { new Secret("secret".Sha256()) }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "cwm.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "myApi.read" }
            }
        };
}