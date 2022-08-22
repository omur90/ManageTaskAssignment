using ManageTaskAssignment.Assignment.Api.Enums;

namespace ManageTaskAssignment.Assignment.Api.Dto
{
    public class GetWorkOrderByEmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public Guid WorkOrderId { get; set; }
        public Guid WorkOrderDetailId { get; set; }
        public string TaskId { get; set; }
        public string DetailsOfTask { get; set; }
        public WorkOrderStatusType Status { get; set; }
        public bool IsOpen { get; set; }
    }
}
