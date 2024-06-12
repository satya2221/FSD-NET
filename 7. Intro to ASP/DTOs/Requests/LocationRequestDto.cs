namespace _7._Intro_to_ASP;

public record LocationRequestDto(
    string StreetAddress,
    string PostalCode,
    string City,
    string StateProvince,
    Guid CountryId
);