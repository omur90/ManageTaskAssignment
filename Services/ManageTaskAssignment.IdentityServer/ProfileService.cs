using IdentityServer4.Models;
using IdentityServer4.Services;
using ManageTaskAssignment.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManageTaskAssignment.IdentityServer
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            if (user == null)
                return;

            var claims = new List<Claim>
            {
                new Claim("fullname", user.UserName),
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Any())
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim("role", role));
                }
            }

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
