using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace EPRPlatform.API.AuthenticationCenter
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("OAApi", "OA"),
                new ApiScope("OtherApi", "Other")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "OAClient",
                    ClientSecrets = { new Secret("OASecret".Sha256()) },
                    AllowedGrantTypes = new []{GrantType.ResourceOwnerPassword },
                    AbsoluteRefreshTokenLifetime = 2592000,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 3600,

                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>{
                        "OAApi",
                        StandardScopes.OfflineAccess,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    },
                },
                new Client
                {
                    ClientId = "OtherClient",
                    ClientSecrets = { new Secret("OtherSecret".Sha256()) },
                    AllowedGrantTypes = new []{GrantType.ResourceOwnerPassword },
                    AllowedScopes = { "OtherApi" },
                }
            };
        }
    }
}
