using AutoMapper;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.Assignment.Api.Entities;

namespace ManageTaskAssignment.Assignment.Api.Mapper
{
    public class WorkOrderMapping : Profile
    {
        public WorkOrderMapping()
        {
            CreateMap<GetWorkOrderDto, WorkOrder>()
                .ForMember(dest => dest.StatusId, from => from.MapFrom(src => $"{(int)src.Status}"))
                .ForMember(dest => dest.WorkOrderDetail.WorkOrderId, from => from.MapFrom(src => src.WorkOrderDetailId))
                .ForMember(dest => dest.Id, from => from.MapFrom(src => src.WorkOrderId))
                .ForMember(dest => dest.EmployeeId, from => from.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.TaskId, from => from.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.IsOpen, from => from.MapFrom(src => src.IsOpen))
                .ForMember(dest => dest.WorkOrderDetail.DetailsOfTask, from => from.MapFrom(src => src.DetailsOfTask))
                .ReverseMap();
        }
    }
}
