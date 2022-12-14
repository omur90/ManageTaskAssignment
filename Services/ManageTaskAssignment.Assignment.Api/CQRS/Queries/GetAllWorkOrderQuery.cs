using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Queries
{
    public class GetAllWorkOrderQuery : IRequest<GenericResponse<List<GetAllWorkOrderDto>>>
    {
    }
}
