namespace _7._Intro_to_ASP;

public record class LocationResponseDto
(
    Guid Id,
    string StreetAddress,
    string PostalCode,
    string City,
    string StateProvince,
    Guid CountryId
);
