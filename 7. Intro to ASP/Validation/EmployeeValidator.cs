using System.Data;
using FluentValidation;

namespace _7._Intro_to_ASP;

public class EmployeeValidator : AbstractValidator<EmployeeRequestDto>
{
    public EmployeeValidator()
    {
        RuleFor(x=> x.FirstName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .NotNull()
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Salary)
            .NotEmpty();
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .Matches("^\\d+$");
        RuleFor(x => x.CommisionPct)
            .NotNull()
            .PrecisionScale(2, 1, true);
        RuleFor(x=> x.JobId)
            .NotEmpty();
        RuleFor(x => x.DepartmentId)
            .NotEmpty();
    }
}
