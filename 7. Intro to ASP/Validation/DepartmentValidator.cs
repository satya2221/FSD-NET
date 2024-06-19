using FluentValidation;
using Models;

namespace _7._Intro_to_ASP;

public class DepartmentValidator : AbstractValidator<DepartmentRequestDto>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(x => x.LocationId)
            .NotNull();
    }
}
