namespace _7._Intro_to_ASP;

public record UserResponseDto
(
    Guid EmployeeId,
    string UserName,
    string Password,
    int Otp,
    DateTime ExpiredOtp,
    bool IsOtpUsed
);


