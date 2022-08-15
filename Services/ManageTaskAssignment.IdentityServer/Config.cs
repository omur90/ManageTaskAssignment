using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace ManageTaskAssignment.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResources.Email(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
          new ApiScope[]
          {
                new ApiScope("employee_fullpermission","Employee API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
          };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_employee"){Scopes={"employee_fullpermission"}}
        };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "WebApp",
                    ClientName = "ManageTaskAssignment Web App",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("52D3E908-5974-4905-9D9D-EA25DC94091F".Sha256()) },
                    AllowedScopes = { "employee_fullpermission" }
                },
                new Client
                {
                    ClientId = "WebAppWithUser",
                    ClientName = "ManageTaskAssignment Web App With Employee",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("2468BD47-4F26-47A2-856F-6974145D1B2C".Sha256()) },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "scope1" },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime =  (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
            };
    }
}