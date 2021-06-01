using IdentityServer4.Models;
using System.Collections.Generic;

namespace WebApplicationShopPlatform.Identity
{
    internal static class Config
    {
        internal static IEnumerable<ApiScope> Scopes => new List<ApiScope>
        {
            new ApiScope("WebApplicationShopPlatform.Identity", "WebApplicationShopPlatform Identity")
        };

        internal static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "9a74d8c6-80d6-4c6a-b99b-7736e104f311",
                AllowedGrantTypes = GrantTypes.ClientCredentials
            }
        };
    }
}
