namespace _7._Intro_to_ASP;

public record ForgotPasswordRequestDto(
    string EmailOrUsername,
    int Otp,
    string Password,
    string ConfirmPassword
);
