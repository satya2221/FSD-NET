namespace _7._Intro_to_ASP;

public record ErrorResponseDto(
    int Code,
    string Message,
    Dictionary<string, string[]?> ErrorDetails
);