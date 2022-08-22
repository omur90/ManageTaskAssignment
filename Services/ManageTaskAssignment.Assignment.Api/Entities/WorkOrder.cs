using System.ComponentModel.DataAnnotations;

namespace ManageTaskAssignment.Assignment.Api.Entities
{
    public class WorkOrder
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string TaskId { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsOpen { get; set; }

        public virtual WorkOrderDetail WorkOrderDetail { get; set; }
    }
}
