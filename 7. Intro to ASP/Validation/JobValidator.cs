using FluentValidation;

namespace _7._Intro_to_ASP;

public class JobValidator : AbstractValidator<JobRequestDto>
{
    public JobValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x=> x.MinSalary)
            .NotEmpty()
            .LessThanOrEqualTo(x=> x.MaxSalary);
        RuleFor(x => x.MaxSalary)
            .NotEmpty()
            .GreaterThanOrEqualTo(x=> x.MinSalary);
    }
}
