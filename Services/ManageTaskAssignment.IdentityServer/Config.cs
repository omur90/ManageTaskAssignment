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
                        new IdentityResource{ Name="roles", DisplayName="Roles", Description="Kullanıcı Rolleri", UserClaims = new[]{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
          new ApiScope[]
          {
                new ApiScope("employee_api_client_permission","Employee API için client erişimi"),
                new ApiScope("employee_api_user_permission","Employee API için user erişimi"),
                new ApiScope("task_api_user_permission","Task API için user erişim"),
                new ApiScope("task_api_client_permission","Task API için client erişim"),
                new ApiScope("assignment_api_user_permission","Assignment API için user erişim"),
                new ApiScope("assignment_api_client_permission","Assignment API için client erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
          };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName), //for IdentityServer.Api
            new ApiResource("resource_employee_api"){Scopes={"employee_api_user_permission", "employee_api_client_permission"}},
            new ApiResource("resource_task_api"){Scopes={"task_api_user_permission", "task_api_client_permission"}},
            new ApiResource("resource_assignment_api"){Scopes={"assignment_api_client_permission", "assignment_api_user_permission"}}
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
                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        "employee_api_client_permission",
                        "task_api_client_permission",
                        "assignment_api_client_permission"
                    }
                },
                new Client
                {
                    ClientId = "WebAppWithUser",
                    ClientName = "ManageTaskAssignment Web App With Employee",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("2468BD47-4F26-47A2-856F-6974145D1B2C".Sha256()) },
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "assignment_api_user_permission",
                        "employee_api_user_permission",
                        "task_api_user_permission",
                        "roles"
                    },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime =  (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
            };
    }
}