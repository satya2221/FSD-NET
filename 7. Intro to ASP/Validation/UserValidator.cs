using System.Data;
using FluentValidation;

namespace _7._Intro_to_ASP;

public class UserValidator : AbstractValidator<UserRequestDto>
{
    public UserValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty();
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(8)
            .Matches("[A-Z]+").WithMessage("Password must contain at least one uppercase")
            .Matches("[a-z]+").WithMessage("Password must contain at least one uppercase")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches(@"[\!\?\*\.]+").WithMessage("Password must contain at least one (!?*.)");
    }
}
