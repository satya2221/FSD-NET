namespace _7._Intro_to_ASP;

public interface IUserService : IGeneralService<UserRequestDto, UserResponseDto>
{
    Task RegisterUserAsync(RegisterRequestDto registerRequestDto);
    Task<string> LoginUserAsync(LoginRequestDto loginRequestDto);
    Task AddUserRoleAsync(UserRoleRequestDto requestDto);
    Task RemoveUserRoleAsync(UserRoleRequestDto requestDto);
}
