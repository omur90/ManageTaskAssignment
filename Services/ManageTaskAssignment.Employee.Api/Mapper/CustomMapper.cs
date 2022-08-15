using AutoMapper;

namespace ManageTaskAssignment.Employee.Api.Mapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<Models.Employee, Dtos.EmployeeDto>().ReverseMap();
        }
    }
}
