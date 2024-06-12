namespace _7._Intro_to_ASP;

public record class JobResponseDto
(   
    Guid Id,
    string Title,
    int MinSalary,
    int MaxSalary
);
