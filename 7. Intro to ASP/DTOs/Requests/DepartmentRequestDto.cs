namespace _7._Intro_to_ASP;

public record  DepartmentRequestDto(
    string Name,
    Guid? ManagerId,
    Guid LocationId
);
