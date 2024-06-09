using Models;

namespace _7._Intro_to_ASP;

public class CountryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public Guid RegionId { get; set; }

    public static explicit operator Country(CountryRequestDto dto)
    {
        return new Country() {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            RegionId = dto.RegionId,
        };
    }
}
