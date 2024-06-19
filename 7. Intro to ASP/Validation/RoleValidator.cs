using System.Data;
using FluentValidation;

namespace _7._Intro_to_ASP;

public class RoleValidator : AbstractValidator<RoleRequestDto>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
