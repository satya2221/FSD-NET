namespace _7._Intro_to_ASP;

public record UserRequestDto(
    Guid EmployeeId,
    string UserName,
    string Password
);
