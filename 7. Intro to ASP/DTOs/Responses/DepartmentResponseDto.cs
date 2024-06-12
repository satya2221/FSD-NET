namespace _7._Intro_to_ASP;

public record  DepartmentResponseDto
(
    Guid Id,
    string Name,
    Guid? ManagerId,
    Guid LocationId
);