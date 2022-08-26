using Microsoft.AspNetCore.Http;

#pragma warning disable CS8602 
namespace ManageTaskAssignment.SharedObjects.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid EmployeeId =>  Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(TokenConstants.SubscriptionClaim).Value);

        public string FullName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == TokenConstants.FullNameClaim).Value;

        public bool IsAdminUser => _httpContextAccessor.HttpContext.User.IsInRole(TokenConstants.Admin);
    }
}
