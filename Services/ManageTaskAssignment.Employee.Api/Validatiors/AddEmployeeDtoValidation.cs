using FluentValidation;

namespace ManageTaskAssignment.Employee.Api.Validatiors
{
    public class AddEmployeeDtoValidation : AbstractValidator<Dtos.EmployeeDto>
    {
        public AddEmployeeDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be null/empty !");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("SurName can not be null/empty !");
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("UserId can not be null/empty !");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber can not be null/empty !");
            RuleFor(x => x.ExpireWorkDate).NotNull().WithMessage("ExpireWorkDate value must be assigned !");
        }
    }
}
