using FluentValidation;

namespace _7._Intro_to_ASP;

public class CountryValidator : AbstractValidator<CountryRequestDto>
{
    public CountryValidator()
    {
        RuleFor(x=> x.Name)
            .NotEmpty()
            .MaximumLength(40);
        RuleFor(x => x.RegionId)
            .NotEmpty();
    }
}
