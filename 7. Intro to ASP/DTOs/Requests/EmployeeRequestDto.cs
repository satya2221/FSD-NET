namespace _7._Intro_to_ASP;

public record EmployeeRequestDto
(
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
