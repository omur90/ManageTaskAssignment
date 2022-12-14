

namespace ManageTaskAssignment.SharedObjects.Services
{
    public interface ISharedIdentityService
    {
        public Guid EmployeeId { get; }
        public string FullName { get; }
        public bool IsAdminUser { get; }
    }
}
