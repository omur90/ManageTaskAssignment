using System.ComponentModel.DataAnnotations;

namespace ManageTaskAssignment.Assignment.Api.Entities
{
    public class WorkOrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public string DetailsOfTask { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }

    }
}
