namespace _7._Intro_to_ASP;

public record  EmployeeResponseDto
(
    Guid Id,
    string Nik,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly HireDate,
    int Salary,
    float? CommisionPct,
    Guid? ManagerId,
    Guid JobId,
    Guid DepartmentId
);
