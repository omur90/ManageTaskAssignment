﻿using Microsoft.AspNetCore.Http;

namespace ManageTaskAssignment.SharedObjects.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid EmployeeId =>  Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("sub").Value);

        public string FullName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=> x.Type == "fullname").Value;
    }
}