using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;

namespace ManageTaskAssignment.Assignment.Api.Services
{
    public interface IWorkOrderService
    {
        Task<GenericResponse<NoContent>> CreateWorkOrderAsync(CreateWorkOrderDto workOrder, CancellationToken cancellationToken);

        Task<GenericResponse<NoContent>> CancelWorkOrderAsync(CancelWorkOrderDto cancelWorkOrder, CancellationToken cancellationToken);

        Task<GenericResponse<NoContent>> CompleteWorkOrderAsync(CompleteWorkOrderDto workOrderDetail, CancellationToken cancellationToken);

        Task<GenericResponse<List<GetWorkOrderDto>>> GetWorkOrdersByEmployeeAsync(CancellationToken cancellationToken);

        Task<GenericResponse<GetWorkOrderDto>> GetWorkOrderByTaskAsync(Guid taskId, CancellationToken cancellationToken);

        Task<GenericResponse<List<GetAllWorkOrderDto>>> GetAllWorkOrderAsync(CancellationToken cancellationToken);

    }
}   
