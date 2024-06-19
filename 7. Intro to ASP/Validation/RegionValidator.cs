using System.Data;
using FluentValidation;

namespace _7._Intro_to_ASP;

public class RegionValidator : AbstractValidator<RegionRequestDto>
{
    public RegionValidator()
    {
        RuleFor(x=> x.Name)
            .NotEmpty().WithMessage("Region Name need to be filled")
            .MaximumLength(25).WithMessage("Region Name can't be more than 25 character");
    }
}
